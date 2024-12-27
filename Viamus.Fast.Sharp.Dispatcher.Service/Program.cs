using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Viamus.Fast.Sharp.Dispatcher.Service;
using Viamus.Fast.Sharp.Dispatcher.Service.Endpoints.V1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi()
    .AddDatabases(builder.Configuration)
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

app.AddTenancyEndpoints();

app.Run();

