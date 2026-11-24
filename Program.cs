using AI4SchoolAgent.Components;
using AI4SchoolAgent.Data;
using AI4SchoolAgent.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AgentCommandInterceptor>();
builder.Services.AddDbContext<SchoolDbContext>((services, options) =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddInterceptors(services.GetRequiredService<AgentCommandInterceptor>()));

builder.Services.AddScoped<AgentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SchoolDbContext>();
    context.Database.Migrate();
}


app.Run();
