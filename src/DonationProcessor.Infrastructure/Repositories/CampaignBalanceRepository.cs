using DonationProcessor.Application.Abstractions;
using DonationProcessor.Domain.Entities;
using DonationProcessor.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DonationProcessor.Infrastructure.Repositories;

public class CampaignBalanceRepository : ICampaignBalanceRepository
{
    private readonly DonationProcessorDbContext _context;

    public CampaignBalanceRepository(DonationProcessorDbContext context)
    {
        _context = context;
    }

    public async Task<CampaignBalance?> GetByCampaignIdAsync(Guid campaignId, CancellationToken cancellationToken = default)
    {
        return await _context.CampaignBalances
            .FirstOrDefaultAsync(x => x.CampaignId == campaignId, cancellationToken);
    }

    public async Task AddAsync(CampaignBalance campaignBalance, CancellationToken cancellationToken = default)
    {
        await _context.CampaignBalances.AddAsync(campaignBalance, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CampaignBalance campaignBalance, CancellationToken cancellationToken = default)
    {
        _context.CampaignBalances.Update(campaignBalance);
        await _context.SaveChangesAsync(cancellationToken);
    }
}