using DonationProcessor.Domain.Entities;
using FluentAssertions;

namespace DonationProcessor.UnitTests.Domain.Entities;

public class DonationTests
{
    [Fact]
    public void Constructor_Should_Create_Donation_When_Data_Is_Valid()
    {
        // Arrange
        var donationId = Guid.NewGuid();
        var campaignId = Guid.NewGuid();
        var donorId = Guid.NewGuid();
        var amount = 100m;

        // Act
        var donation = new Donation(donationId, campaignId, donorId, amount);

        // Assert
        donation.Id.Should().Be(donationId);
        donation.CampaignId.Should().Be(campaignId);
        donation.DonorId.Should().Be(donorId);
        donation.Amount.Should().Be(amount);
        donation.ProcessedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Constructor_Should_Throw_When_DonationId_Is_Empty()
    {
        var act = () => new Donation(Guid.Empty, Guid.NewGuid(), Guid.NewGuid(), 100m);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Donation id is required.");
    }

    [Fact]
    public void Constructor_Should_Throw_When_CampaignId_Is_Empty()
    {
        var act = () => new Donation(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), 100m);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Campaign id is required.");
    }

    [Fact]
    public void Constructor_Should_Throw_When_DonorId_Is_Empty()
    {
        var act = () => new Donation(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, 100m);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Donor id is required.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Constructor_Should_Throw_When_Amount_Is_Invalid(decimal amount)
    {
        var act = () => new Donation(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), amount);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Donation amount must be greater than zero.");
    }
}