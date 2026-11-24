using System;
using System.Text;
using AI4SchoolAgent.Data;
using AI4SchoolAgent.Tools;
using GitHub.Copilot;
using Microsoft.Extensions.AI;

namespace AI4SchoolAgent.Services;

public class AgentService
{
    private readonly SchoolDbContext _db;
    private readonly ILogger<AgentService> _logger;
    private readonly AgentCommandInterceptor _commandInterceptor;

    public AgentService(SchoolDbContext db, ILogger<AgentService> logger, AgentCommandInterceptor commandInterceptor)
    {
        _logger = logger;
        _db = db;
        _commandInterceptor = commandInterceptor;
    }

    /// <summary>Lists the models available to the server's Copilot account.</summary>
    /// <returns>The model catalog reported by Copilot.</returns>
    public async Task<IList<ModelInfo>> ListModelsAsync()
    {
        await using var client = CreateClient();
        await client.StartAsync();
        return await client.ListModelsAsync();
    }

    /// <summary>Sends a prompt to the school agent using the selected model.</summary>
    /// <param name="prompt">The user's message.</param>
    /// <param name="modelId">The Copilot model ID, or <c>auto</c> for automatic selection.</param>
    /// <param name="activity">Receives tool calls and SQL command text for this request.</param>
    /// <returns>The agent's response.</returns>
    public async Task<string> AskAsync(string prompt, string modelId = "auto", IProgress<AgentActivity>? activity = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(modelId);

        await using var client = CreateClient();
        await client.StartAsync();

        var toolService = new StudentsAgentTools(_db);

        var tools = new List<AIFunctionDeclaration> {
            AIFunctionFactory.Create(
                toolService.FindStudentsByAnyFilterAsync,
                "find_students_by_any_filter",
                "Search students by name (first OR last), firstName, lastName, school, and/or birthYear. " +
                "Supplied filters combine with AND; provide at least one. Text matching is case-insensitive, " +
                "ignores surrounding input whitespace, and uses matchMode exact (default) or contains. " +
                "Use the stored school name, e.g. Computing rather than School of Computing."),

            AIFunctionFactory.Create(
                async (int studentId) =>
                    await toolService.GetStudentDetailsByIdAsync(studentId),
                "get_student_details",
                "Look up student details."),

            AIFunctionFactory.Create(
                async (int studentId, string reason) =>
                    await toolService.ProcessWithdrawalAsync(studentId, reason),
                "process_withdrawal",
                "Process a withdrawal request."),

            AIFunctionFactory.Create(
                async (string gender) =>
                    await toolService.GetStudentsByGenderAsync(gender),
                "get_students_by_gender",
                "Find students by gender"),

            AIFunctionFactory.Create(
                async () =>
                    await toolService.GetStudentsByAgeAsync(),
                "get_students_by_age",
                "Find students by age"),

            AIFunctionFactory.Create(
                async () =>
                    await toolService.GetAllStudentsAsync(),
                "get_all_students",
                "Get all students")
        };

        var config = new SessionConfig
        {
            Model = modelId,
            OnPermissionRequest = PermissionHandler.ApproveAll,
            Tools = tools
        };

        await using var session = await client.CreateSessionAsync(config);

        var responseBuilder = new StringBuilder();
        var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

        using var subscription = session.On<SessionEvent>(evt =>
        {
            switch (evt)
            {
                case AssistantMessageEvent msg when !string.IsNullOrWhiteSpace(msg.Data.Content):
                    if (responseBuilder.Length > 0)
                        responseBuilder.AppendLine().AppendLine();

                    responseBuilder.Append(msg.Data.Content);
                    break;

                case SessionIdleEvent:
                    tcs.TrySetResult(responseBuilder.ToString());
                    break;

                case SessionErrorEvent error:
                    tcs.TrySetException(new InvalidOperationException(error.Data.Message));
                    break;
                    
                case ToolExecutionStartEvent tool:
                    _logger.LogInformation(
                        "Executing tool: {ToolName}",
                        tool.Data.ToolName);
                    activity?.Report(new AgentActivity("Tool called", tool.Data.ToolName));
                    break;
            }
        });

        _commandInterceptor.CommandExecuting = sql => activity?.Report(new AgentActivity("SQL statement", sql));
        try
        {
            await session.SendAsync(new MessageOptions
            {
                Prompt = prompt
            });

            return await tcs.Task.WaitAsync(
                TimeSpan.FromSeconds(30));
        }
        finally
        {
            _commandInterceptor.CommandExecuting = null;
        }
    }

    private static CopilotClient CreateClient() =>
        new(new CopilotClientOptions { 
            LogLevel = CopilotLogLevel.Info,

            // next two lines authenticates with personal access token
            GitHubToken = Environment.GetEnvironmentVariable("COPILOT_GITHUB_TOKEN"),
            UseLoggedInUser = false   // explicit token only; stored login & gh ignored
        }
    );

}

public record AgentActivity(string Label, string Content);

