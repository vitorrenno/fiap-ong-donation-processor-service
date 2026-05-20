using FluentValidation;

namespace IdentityCampaign.Application.Features.Donation.GetDonationMe
{
    public class GetDonationMeValidator : AbstractValidator<GetDonationMeCommand>
    {
        public GetDonationMeValidator()
        {

            RuleFor(command => command.IdUser)
                    .NotEmpty()
                    .WithMessage("Id user is required");
        }

    }
}
