using DonationProcessor.Application.Abstractions;
using DonationProcessor.Application.Features.Donations.ProcessDonation;
using DonationProcessor.Infrastructure.Persistence;
using DonationProcessor.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.AddDbContext<DonationProcessorDbContext>(options =>
    options.UseInMemoryDatabase("DonationProcessorDb"));

builder.Services.AddScoped<IDonationRepository, DonationRepository>();
builder.Services.AddScoped<ICampaignBalanceRepository, CampaignBalanceRepository>();

builder.Services.AddMediatR(typeof(ProcessDonationHandler).Assembly);

var host = builder.Build();

await host.RunAsync();