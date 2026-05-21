using FluentValidation;

namespace DonationProcessor.Application.Features.Donations.GetDonationById
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
