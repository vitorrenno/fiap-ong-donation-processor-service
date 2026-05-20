using MediatR;

namespace IdentityCampaign.Application.Features.Donation.GetDonationById
{
    public sealed record GetDonationByIdCommand(Guid Id) : IRequest<GetDonationByIdResponse> { }
}
