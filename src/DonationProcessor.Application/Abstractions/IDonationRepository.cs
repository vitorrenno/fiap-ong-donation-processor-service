using DonationProcessor.Domain.Entities;

namespace DonationProcessor.Application.Abstractions;

public interface IDonationRepository
{
    Task AddAsync(Donation donation, CancellationToken cancellationToken = default);
    Task<Donation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}