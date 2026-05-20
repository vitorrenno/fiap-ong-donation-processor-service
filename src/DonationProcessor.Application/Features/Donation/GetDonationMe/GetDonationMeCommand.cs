using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityCampaign.Application.Features.Donation.GetDonationById;
using MediatR;

namespace IdentityCampaign.Application.Features.Donation.GetDonationMe
{
    public sealed record GetDonationMeCommand(Guid IdUser) : IRequest<IEnumerable<GetDonationMeResponse>> { }
}
