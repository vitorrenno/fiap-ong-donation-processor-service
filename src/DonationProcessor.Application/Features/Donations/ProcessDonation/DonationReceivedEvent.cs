namespace DonationProcessor.Application.Features.Donations.ProcessDonation;

public sealed record DonationReceivedEvent(
    Guid DonationId,
    Guid CampaignId,
    Guid DonorId,
    decimal Amount,
    DateTime OccurredAt
);