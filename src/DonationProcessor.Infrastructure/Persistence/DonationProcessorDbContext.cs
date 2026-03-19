using DonationProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DonationProcessor.Infrastructure.Persistence;

public class DonationProcessorDbContext : DbContext
{
    public DonationProcessorDbContext(DbContextOptions<DonationProcessorDbContext> options)
        : base(options)
    {
    }

    public DbSet<Donation> Donations => Set<Donation>();
    public DbSet<CampaignBalance> CampaignBalances => Set<CampaignBalance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DonationProcessorDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}