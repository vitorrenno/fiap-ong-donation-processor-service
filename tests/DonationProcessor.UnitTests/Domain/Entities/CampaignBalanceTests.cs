using DonationProcessor.Domain.Entities;
using FluentAssertions;

namespace DonationProcessor.UnitTests.Domain.Entities;

public class CampaignBalanceTests
{
    [Fact]
    public void Constructor_Should_Create_CampaignBalance_When_CampaignId_Is_Valid()
    {
        // Arrange
        var campaignId = Guid.NewGuid();

        // Act
        var campaignBalance = new CampaignBalance(campaignId);

        // Assert
        campaignBalance.CampaignId.Should().Be(campaignId);
        campaignBalance.TotalAmountRaised.Should().Be(0);
    }

    [Fact]
    public void Constructor_Should_Throw_When_CampaignId_Is_Empty()
    {
        var act = () => new CampaignBalance(Guid.Empty);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Campaign id is required.");
    }

    [Fact]
    public void AddAmount_Should_Add_Value_When_Amount_Is_Valid()
    {
        // Arrange
        var campaignBalance = new CampaignBalance(Guid.NewGuid());

        // Act
        campaignBalance.AddAmount(50m);
        campaignBalance.AddAmount(25m);

        // Assert
        campaignBalance.TotalAmountRaised.Should().Be(75m);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void AddAmount_Should_Throw_When_Amount_Is_Invalid(decimal amount)
    {
        // Arrange
        var campaignBalance = new CampaignBalance(Guid.NewGuid());

        // Act
        var act = () => campaignBalance.AddAmount(amount);

        // Assert
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Amount must be greater than zero.");
    }
}