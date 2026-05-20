using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityCampaign.Application.DTOs.Donation
{
    public class CreateDonation
    {
        public decimal vAmount { get; set; }
        public Guid IdCampaign { get; set; }
        public Guid IdUser { get; set; }

    }
}
