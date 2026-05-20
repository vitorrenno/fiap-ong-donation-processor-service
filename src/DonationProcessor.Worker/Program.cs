using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHealthChecks();


var host = builder.Build();

await host.RunAsync();