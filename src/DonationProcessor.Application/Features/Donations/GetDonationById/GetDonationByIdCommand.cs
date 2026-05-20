using MediatR;

namespace DonationProcessor.Application.Features.Donations.GetDonationById
{
    public sealed record GetDonationByIdCommand(Guid Id) : IRequest<GetDonationByIdResponse> { }
}
