using MediatR;

namespace DonationProcessor.Application.Features.Donations.GetAllDonation
{
    public sealed record  GetAllDonationCommand : IRequest<IEnumerable<GetAllDonationResponse>>
    {
    }
}
