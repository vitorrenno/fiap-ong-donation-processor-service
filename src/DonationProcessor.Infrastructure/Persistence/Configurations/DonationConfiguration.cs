using DonationProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonationProcessor.Infrastructure.Persistence.Configurations;

public class DonationConfiguration : IEntityTypeConfiguration<Donation>
{
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CampaignId)
            .IsRequired();

        builder.Property(x => x.DonorId)
            .IsRequired();

        builder.Property(x => x.Amount);

        builder.Property(x => x.ProcessedAt)
            .IsRequired();
    }
}