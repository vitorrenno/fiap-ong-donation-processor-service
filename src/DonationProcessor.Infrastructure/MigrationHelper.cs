using DonationProcessor.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace DonationProcessor.Infrastructure;

public static class MigrationHelper
{
    public static void ApplyMigrations(IApplicationBuilder app)
    {
        var maxRetries = 5;
        var retryCount = 0;

        while (retryCount < maxRetries)
        {
            try
            {
                using var scope = app.ApplicationServices.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<DonationProcessorDbContext>();

                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Applying EF Core migrations...");

                db.Database.Migrate();

                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Migrations applied successfully.");
                return;
            }
            catch (Exception ex)
            {
                retryCount++;

                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Migration attempt {retryCount} failed: {ex.Message}");

                if (retryCount >= maxRetries)
                    throw;

                Thread.Sleep((int)Math.Pow(2, retryCount) * 1000);
            }
        }
    }

    // Wait for Docker to start up before applying migrations automatically.
    public static async Task WaitForMySqlAsync(string connectionString)
    {
        var maxRetries = 30;
        var delaySeconds = 5;

        string[] strConn = connectionString.Split(";");
        for (int i = 1; i <= maxRetries; i++)
        {
            try
            {
                //RETIRANDO O NOME DO DATABASE POIS ELE PODE NÃO EXISTIR
                connectionString = $"{strConn[0]};{strConn[1]};{strConn[3]};{strConn[4]};";
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();
                Console.WriteLine("MySQL está pronto!");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} Tentativa {i}/{maxRetries} falhou: {ex.Message}");
                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }

        throw new Exception("MySQL não ficou pronto dentro do tempo esperado.");
    }

}
