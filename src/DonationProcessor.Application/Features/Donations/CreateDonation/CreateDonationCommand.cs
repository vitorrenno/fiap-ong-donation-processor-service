using MediatR;

namespace DonationProcessor.Application.Features.Donations.CreateDonation
{
    public sealed record CreateDonationCommand : IRequest<CreateDonationResponse>
    {
        public decimal vAmount { get; set; }
        public Guid IdCampaign { get; set; }
        public Guid IdUser { get; set; }
    }
}
