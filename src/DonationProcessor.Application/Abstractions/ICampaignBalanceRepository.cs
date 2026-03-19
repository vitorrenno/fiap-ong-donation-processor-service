using DonationProcessor.Domain.Entities;

namespace DonationProcessor.Application.Abstractions;

public interface ICampaignBalanceRepository
{
    Task<CampaignBalance?> GetByCampaignIdAsync(Guid campaignId, CancellationToken cancellationToken = default);
    Task AddAsync(CampaignBalance campaignBalance, CancellationToken cancellationToken = default);
    Task UpdateAsync(CampaignBalance campaignBalance, CancellationToken cancellationToken = default);
}