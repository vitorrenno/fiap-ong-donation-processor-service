using DonationProcessor.Application.Abstractions;
using DonationProcessor.Domain.Entities;
using DonationProcessor.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DonationProcessor.Infrastructure.Repositories;

public class DonationRepository : IDonationRepository
{
    private readonly DonationProcessorDbContext _context;

    public DonationRepository(DonationProcessorDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Donation donation, CancellationToken cancellationToken = default)
    {
        await _context.Donations.AddAsync(donation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Donation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Donations.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}