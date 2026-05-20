using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace IdentityCampaign.Application.Features.Donation.CreateDonation
{
    public class CreateDonationValidator : AbstractValidator<CreateDonationCommand>
    {
        public CreateDonationValidator()
        {
            // Valida se o valor da doação foi informado
            RuleFor(command => command.vAmount)
                .NotNull()
                .WithMessage("Donation amount is required.");

            // Valida se o valor da doação é maior que zero
            RuleFor(command => command.vAmount)
                .GreaterThan(0)
                .WithMessage("Donation amount must be greater than zero.");

            // Valida se o valor mínimo permitido foi atingido
            RuleFor(command => command.vAmount)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Minimum donation amount is 1.");

            // Valida se o valor máximo permitido não foi excedido
            RuleFor(command => command.vAmount)
                .LessThanOrEqualTo(1000000)
                .WithMessage("Donation amount exceeds the allowed limit.");

            // Valida se o identificador do usuário foi informado
            RuleFor(command => command.IdUser)
                .NotEmpty()
                .WithMessage("User identifier is required.");

            // Valida se o identificador da campanha foi informado
            RuleFor(command => command.IdCampaign)
                .NotEmpty()
                .WithMessage("Campaign identifier is required.");

            // Valida se usuário e campanha não possuem o mesmo identificador
            RuleFor(command => command)
                .Must(command => command.IdUser != command.IdCampaign)
                .WithMessage("User identifier cannot be the same as campaign identifier.");
        }
    }
}
