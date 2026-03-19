using DonationProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationProcessor.Infrastructure.Persistence.Configurations;

public class CampaignBalanceConfiguration : IEntityTypeConfiguration<CampaignBalance>
{
    public void Configure(EntityTypeBuilder<CampaignBalance> builder)
    {
        builder.HasKey(x => x.CampaignId);

        builder.Property(x => x.TotalAmountRaised);
    }
}