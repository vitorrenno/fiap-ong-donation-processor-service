using DonationProcessor.Application.Abstractions;
using DonationProcessor.Application.Features.Donations.CreateDonation;
using DonationProcessor.Application.Features.Donations.GetAllDonation;
using DonationProcessor.Application.Features.Donations.GetDonationById;
using DonationProcessor.Application.Features.Donations.GetDonationMe;
using DonationProcessor.Application.MapperProfile;
using DonationProcessor.Application.Messaging.Events;
using DonationProcessor.Infrastructure.Persistence;
using DonationProcessor.Infrastructure.Repositories;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DonationProcessorDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

#region Interfaces
builder.Services.AddScoped<IDonationRepository, DonationRepository>();
#endregion

#region MediatR
//Donation
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDonationCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetDonationByIdCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllDonationCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetDonationMeCommandHandler).Assembly));

#endregion

#region AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(DonationProfile).Assembly);
#endregion

#region Validators
//Donation
builder.Services.AddScoped<IValidator<CreateDonationCommand>, CreateDonationValidator>();
builder.Services.AddScoped<IValidator<GetDonationByIdCommand>, GetDonationByIdValidator>();
builder.Services.AddScoped<IValidator<GetAllDonationCommand>, GetAllDonationValidator>();
builder.Services.AddScoped<IValidator<GetDonationMeCommand>, GetDonationMeValidator>();

#endregion

#region MassTransit

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]!);
            h.Password(builder.Configuration["RabbitMQ:Password"]!);
        });

        cfg.Message<DonationReceivedEvent>(m =>
        {
            m.SetEntityName("donation-received");
        });
    });
});

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

#region Banco de dados 
var environment = builder.Environment.EnvironmentName;
var connString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ========================================");
Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Iniciando aplicação em ambiente: {environment}");
Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ========================================");

if (environment == Environments.Development)
{
    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Modo DESENVOLVIMENTO detectado.");

    // Apenas para DEV LOCAL (fora do K8s)
    try
    {
        await Api.Services.DockerMySqlManager.EnsureMySqlContainerRunningAsync(connString);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ⚠ Aviso: Não foi possível gerenciar container Docker. Se está em K8s, isso é esperado. Erro: {ex.Message}");
    }

    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Aguardando MySQL estar pronto...");
    await DonationProcessor.Infrastructure.MigrationHelper.WaitForMySqlAsync(connString);

    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Aplicando migrations...");
    DonationProcessor.Infrastructure.MigrationHelper.ApplyMigrations(app);
}
else
{
    // Kubernetes / Staging / Production
    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Modo PRODUÇÃO/KUBERNETES detectado.");
    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Aguardando MySQL estar pronto...");

    try
    {
        await DonationProcessor.Infrastructure.MigrationHelper.WaitForMySqlAsync(connString);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ✗ ERRO CRÍTICO ao conectar no MySQL: {ex.Message}");
        throw;
    }

    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Aplicando migrations...");
    DonationProcessor.Infrastructure.MigrationHelper.ApplyMigrations(app);
}

Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ========================================");
Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Aplicação iniciada com sucesso!");
Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ========================================");
#endregion

app.Run();
