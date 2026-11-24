Yes, you can run this app on Azure. A personal access token is one 
option—not a requirement. Your local Copilot login does not 
automatically transfer to Azure.

Option 1: Fine-grained PAT—for a private lab/demo
=================================================

This is the simplest approach for your current app:

1) In GitHub, create a fine-grained personal access token:
- Choose your personal account as resource owner.
- Grant the Copilot Requests account permission.
- Set an expiration date.

2) In Azure App Service, add this environment variable:

COPILOT_GITHUB_TOKEN=<your-token>

Prefer storing the token in Azure Key Vault and using a Key Vault reference.

3) Deploy the application with the SDK’s compatible Copilot runtime/CLI included.

Your existing client creation in AgentService.cs can read that environment variable automatically—no authentication code change is needed.

The account needs Copilot access; requests use that account’s applicable usage allowances and billing. Classic PATs (ghp_) are not supported. See GitHub’s authentication documentation.

Test locally:

cd /Users/medhatelmasry/_PlayGround/0000/sdk101/AI4SchoolAgent
COPILOT_GITHUB_TOKEN=github_pat_xxxxxxxx dotnet run

Option 2: GitHub OAuth—for users with Copilot accounts
======================================================

Let each user sign in with GitHub and pass their token to the SDK:

var client = new CopilotClient(new CopilotClientOptions
{
    GitHubToken = userAccessToken,
    UseLoggedInUser = false
});

Each user uses their own Copilot entitlement. Your app handles secure token storage, expiration, and refresh. See GitHub OAuth setup.

Option 3: Azure-hosted model—for a school application
=====================================================

The SDK also supports BYOK, using an Azure-hosted model instead of GitHub-hosted Copilot inference:
- No personal GitHub token or Copilot subscription is required for model access.
- Model usage is billed through Azure.
- Azure Managed Identity can authenticate without storing an API key.
- Your session configuration must specify the provider and deployed model; the current Copilot model-listing UI would also need adjustment.

See Azure Managed Identity with the Copilot SDK.

Recommendation: use a fine-grained PAT for your private lab. For a deployed school application, prefer an Azure-hosted model with Managed Identity rather than routing everyone through your personal Copilot account.

Before exposing the app publicly, also replace PermissionHandler.ApproveAll with an appropriate permission policy and enforce application-level authorization—especially for student data and withdrawals.

