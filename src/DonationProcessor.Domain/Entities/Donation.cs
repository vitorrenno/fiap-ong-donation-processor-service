namespace DonationProcessor.Domain.Entities;

public class Donation
{
    public Guid Id { get; private set; }
    public Guid CampaignId { get; private set; }
    public Guid DonorId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime ProcessedAt { get; private set; }

    private Donation()
    {
    }

    public Donation(Guid id, Guid campaignId, Guid donorId, decimal amount)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Donation id is required.");

        if (campaignId == Guid.Empty)
            throw new ArgumentException("Campaign id is required.");

        if (donorId == Guid.Empty)
            throw new ArgumentException("Donor id is required.");

        if (amount <= 0)
            throw new ArgumentException("Donation amount must be greater than zero.");

        Id = id;
        CampaignId = campaignId;
        DonorId = donorId;
        Amount = amount;
        ProcessedAt = DateTime.UtcNow;
    }
}