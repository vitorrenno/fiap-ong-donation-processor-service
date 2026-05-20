using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Docker.DotNet.Models;
using IdentityCampaign.Application.Features.Donation.CreateDonation;
using IdentityCampaign.Domain.Entities;

namespace IdentityCampaign.IntegrationTests.Commands.Donation
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
