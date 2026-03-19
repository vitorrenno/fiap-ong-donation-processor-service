using DonationProcessor.Application.Abstractions;
using DonationProcessor.Domain.Entities;
using MediatR;

namespace DonationProcessor.Application.Features.Donations.ProcessDonation;

public sealed class ProcessDonationHandler : IRequestHandler<ProcessDonationCommand, Unit>
{
    private readonly IDonationRepository _donationRepository;
    private readonly ICampaignBalanceRepository _campaignBalanceRepository;

    public ProcessDonationHandler(
        IDonationRepository donationRepository,
        ICampaignBalanceRepository campaignBalanceRepository)
    {
        _donationRepository = donationRepository;
        _campaignBalanceRepository = campaignBalanceRepository;
    }

    public async Task<Unit> Handle(
        ProcessDonationCommand request,
        CancellationToken cancellationToken)
    {
        var existingDonation = await _donationRepository.GetByIdAsync(request.DonationId, cancellationToken);

        if (existingDonation is not null)
            return Unit.Value;

        var donation = new Donation(
            request.DonationId,
            request.CampaignId,
            request.DonorId,
            request.Amount);

        await _donationRepository.AddAsync(donation, cancellationToken);

        var campaignBalance = await _campaignBalanceRepository.GetByCampaignIdAsync(
            request.CampaignId,
            cancellationToken);

        if (campaignBalance is null)
        {
            campaignBalance = new CampaignBalance(request.CampaignId);
            campaignBalance.AddAmount(request.Amount);

            await _campaignBalanceRepository.AddAsync(campaignBalance, cancellationToken);
            return Unit.Value;
        }

        campaignBalance.AddAmount(request.Amount);
        await _campaignBalanceRepository.UpdateAsync(campaignBalance, cancellationToken);

        return Unit.Value;
    }
}