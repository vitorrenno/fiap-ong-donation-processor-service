using Bogus;
using DonationProcessor.Application.Features.Donations.GetDonationMe;

namespace DonationProcessor.IntegrationTests.Commands.Donation
{
    public class GetDonationMeCommandBuilder
    {
        public static GetDonationMeCommand Build()
        {
            return new Faker<GetDonationMeCommand>()
                .CustomInstantiator(faker => new GetDonationMeCommand(IdUser: Guid.NewGuid()));
        }

    }
}
