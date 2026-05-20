using MediatR;

namespace IdentityCampaign.Application.Features.Donation.GetAllDonation
{
    public sealed record  GetAllDonationCommand : IRequest<IEnumerable<GetAllDonationResponse>>
    {
    }
}
