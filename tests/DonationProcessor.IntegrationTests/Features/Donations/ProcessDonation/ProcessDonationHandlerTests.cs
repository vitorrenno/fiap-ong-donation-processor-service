using DonationProcessor.Application.Features.Donations.ProcessDonation;
using DonationProcessor.Domain.Entities;
using DonationProcessor.Infrastructure.Persistence;
using DonationProcessor.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DonationProcessor.IntegrationTests.Features.Donations.ProcessDonation;

public class ProcessDonationHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Donation_And_CampaignBalance_When_First_Donation_Is_Processed()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DonationProcessorDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new DonationProcessorDbContext(options);

        var donationRepository = new DonationRepository(context);
        var campaignBalanceRepository = new CampaignBalanceRepository(context);

        var handler = new ProcessDonationHandler(
            donationRepository,
            campaignBalanceRepository);

        var command = new ProcessDonationCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            150.75m,
            DateTime.UtcNow);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var donation = await context.Donations.FirstOrDefaultAsync(x => x.Id == command.DonationId);
        donation.Should().NotBeNull();
        donation!.Amount.Should().Be(150.75m);

        var balance = await context.CampaignBalances.FirstOrDefaultAsync(x => x.CampaignId == command.CampaignId);
        balance.Should().NotBeNull();
        balance!.TotalAmountRaised.Should().Be(150.75m);
    }

    [Fact]
    public async Task Handle_Should_Update_CampaignBalance_When_Campaign_Already_Has_Donations()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DonationProcessorDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new DonationProcessorDbContext(options);

        var donationRepository = new DonationRepository(context);
        var campaignBalanceRepository = new CampaignBalanceRepository(context);

        var campaignId = Guid.NewGuid();

        var existingBalance = new CampaignBalance(campaignId);
        existingBalance.AddAmount(100m);

        await context.CampaignBalances.AddAsync(existingBalance);
        await context.SaveChangesAsync();

        var handler = new ProcessDonationHandler(
            donationRepository,
            campaignBalanceRepository);

        var command = new ProcessDonationCommand(
            Guid.NewGuid(),
            campaignId,
            Guid.NewGuid(),
            50m,
            DateTime.UtcNow);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var balance = await context.CampaignBalances.FirstAsync(x => x.CampaignId == campaignId);
        balance.TotalAmountRaised.Should().Be(150m);
    }

    [Fact]
    public async Task Handle_Should_Not_Process_Duplicate_Donation()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DonationProcessorDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new DonationProcessorDbContext(options);

        var donationRepository = new DonationRepository(context);
        var campaignBalanceRepository = new CampaignBalanceRepository(context);

        var donationId = Guid.NewGuid();
        var campaignId = Guid.NewGuid();
        var donorId = Guid.NewGuid();

        var handler = new ProcessDonationHandler(
            donationRepository,
            campaignBalanceRepository);

        var command = new ProcessDonationCommand(
            donationId,
            campaignId,
            donorId,
            25m,
            DateTime.UtcNow);

        // Act
        await handler.Handle(command, CancellationToken.None);
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var donations = await context.Donations
            .Where(x => x.Id == donationId)
            .ToListAsync();

        donations.Should().HaveCount(1);

        var balance = await context.CampaignBalances.FirstAsync(x => x.CampaignId == campaignId);
        balance.TotalAmountRaised.Should().Be(25m);
    }
}