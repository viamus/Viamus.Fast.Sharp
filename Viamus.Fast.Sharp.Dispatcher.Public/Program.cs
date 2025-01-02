using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Viamus.Fast.Sharp.Dispatcher.Public;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi()
    .AddDatabases(builder.Configuration)
    .AddHybridCaching(builder.Configuration)
    .AddHandlers()
    .AddHealthChecks(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

