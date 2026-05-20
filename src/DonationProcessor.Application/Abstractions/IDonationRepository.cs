using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityCampaign.Domain.Entities;

namespace IdentityCampaign.Application.Abstractions
{
    public interface IDonationRepository
    {
        Task AddAsync(Donation donation, CancellationToken cancellationToken = default);

        Task<Donation> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Donation>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Donation>> GetMeAsync(Guid idUser, CancellationToken cancellationToken = default);

    }
}
