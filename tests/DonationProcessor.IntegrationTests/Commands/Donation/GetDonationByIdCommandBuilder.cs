using Bogus;
using DonationProcessor.Application.Features.Donations.GetDonationById;

namespace DonationProcessor.IntegrationTests.Commands.Donation
{
    public class GetDonationByIdCommandBuilder
    {
        public static GetDonationByIdCommand Build()
        {
            return new Faker<GetDonationByIdCommand>()
                .CustomInstantiator(faker => new GetDonationByIdCommand(Id: Guid.NewGuid()));
        }
    }

}

