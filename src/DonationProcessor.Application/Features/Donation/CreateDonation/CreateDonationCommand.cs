using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace IdentityCampaign.Application.Features.Donation.CreateDonation
{
    public sealed record CreateDonationCommand : IRequest<CreateDonationResponse>
    {
        public decimal vAmount { get; set; }
        public Guid IdCampaign { get; set; }
        public Guid IdUser { get; set; }
    }
}
