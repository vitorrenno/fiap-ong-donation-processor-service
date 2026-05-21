using MediatR;

namespace DonationProcessor.Application.Features.Donations.GetDonationMe
{
    public sealed record GetDonationMeCommand(Guid IdUser) : IRequest<IEnumerable<GetDonationMeResponse>> { }
}
