using MediatR;

namespace DonationProcessor.Application.Features.Donations.ProcessDonation;

public sealed record ProcessDonationCommand(
    Guid DonationId,
    Guid CampaignId,
    Guid DonorId,
    decimal Amount,
    DateTime OccurredAt
) : IRequest<Unit>;