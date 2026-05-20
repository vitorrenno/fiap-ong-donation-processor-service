using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using IdentityCampaign.Application.Features.Donation.CreateDonation;
using IdentityCampaign.Application.Features.Donation.GetDonationById;

namespace IdentityCampaign.IntegrationTests.Commands.Donation
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

