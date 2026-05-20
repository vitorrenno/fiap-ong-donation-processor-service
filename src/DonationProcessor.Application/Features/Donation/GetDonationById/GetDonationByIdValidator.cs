using FluentValidation;

namespace IdentityCampaign.Application.Features.Donation.GetDonationById
{
    public class GetDonationByIdValidator : AbstractValidator<GetDonationByIdCommand>
    {
        public GetDonationByIdValidator() {

            RuleFor(command => command.Id)
                    .NotEmpty()
                    .WithMessage("Id donation is required");
        }
    }
}
