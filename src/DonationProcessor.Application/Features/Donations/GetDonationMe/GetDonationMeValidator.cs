using FluentValidation;

namespace DonationProcessor.Application.Features.Donations.GetDonationMe
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
