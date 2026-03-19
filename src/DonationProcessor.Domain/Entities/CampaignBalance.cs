namespace DonationProcessor.Domain.Entities;

public class CampaignBalance
{
    public Guid CampaignId { get; private set; }
    public decimal TotalAmountRaised { get; private set; }

    private CampaignBalance()
    {
    }

    public CampaignBalance(Guid campaignId)
    {
        if (campaignId == Guid.Empty)
            throw new ArgumentException("Campaign id is required.");

        CampaignId = campaignId;
        TotalAmountRaised = 0;
    }

    public void AddAmount(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        TotalAmountRaised += amount;
    }
}