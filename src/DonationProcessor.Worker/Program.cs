using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.worker.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.worker.json", optional: true);

builder.Services.AddHealthChecks();


var host = builder.Build();

await host.RunAsync();