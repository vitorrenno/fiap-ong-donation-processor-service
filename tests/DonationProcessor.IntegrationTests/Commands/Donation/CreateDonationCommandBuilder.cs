using Bogus;
using DonationProcessor.Application.Features.Donations.CreateDonation;

namespace DonationProcessor.IntegrationTests.Commands.Donation
{
    public class CreateDonationCommandBuilder
    {
        public static CreateDonationCommand Build()
        {
            var faker = new Faker<CreateDonationCommand>()
                .RuleFor(x => x.vAmount, f => f.Finance.Amount(1, 1000))
                .RuleFor(x => x.IdCampaign, f => f.Random.Guid())
                .RuleFor(x => x.IdUser, f => f.Random.Guid());

            return faker.Generate();
        }
    }
}
