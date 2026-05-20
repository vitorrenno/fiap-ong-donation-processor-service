using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using IdentityCampaign.Application.Features.Donation.GetDonationById;
using IdentityCampaign.Application.Features.Donation.GetDonationMe;

namespace IdentityCampaign.IntegrationTests.Commands.Donation
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
