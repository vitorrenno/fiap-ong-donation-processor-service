using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityCampaign.Application.Features.Donation.CreateDonation
{
    public class CreateDonationResponse
    {
        public Guid Id { get; set; }
        public DateTime dateDonated { get; set; }
        public decimal vAmount { get; set; }
        public Guid IdCampaign { get; set; }
        public Guid IdUser { get; set; }

    }
}
