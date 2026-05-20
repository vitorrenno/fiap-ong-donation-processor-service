using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationProcessor.Domain.Entities;

public class Donation
{
    public Guid Id { get; set; }
    public DateTime dateDonated { get; set; }
    public decimal vAmount { get; set; }
    public Guid IdCampaign { get; set; }
    public Guid IdUser { get; set; }
    public Donation() { }
    public Donation(decimal vAmount, Guid IdCampaign, Guid idUser)
    {
        this.Id = Guid.NewGuid();
        this.dateDonated = DateTime.Now;
        this.vAmount = vAmount;
        this.IdCampaign = IdCampaign;
        this.IdUser = idUser;
    }
}


