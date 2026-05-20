using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DonationProcessor.Infrastructure.Persistence;
using IdentityCampaign.Application.Abstractions;
using IdentityCampaign.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityCampaign.Infrastructure.Repositories
{
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

        public async Task<IReadOnlyList<Donation>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Donations
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
        }

        public async Task<Donation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
           return await _context.Donations.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Donation>> GetMeAsync(Guid idUser, CancellationToken cancellationToken = default)
        {
            return await _context.Donations
                        .Where(x=>x.IdUser==idUser)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
        }
    }
}
