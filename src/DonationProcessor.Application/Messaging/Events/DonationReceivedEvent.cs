namespace DonationProcessor.Application.Messaging.Events;

public record DonationReceivedEvent(
    Guid DonationId,
    Guid CampaignId,
    decimal DonationValue,
    DateTime CreatedAt);