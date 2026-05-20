using AutoMapper;
using IdentityCampaign.Application.DTOs.Donation;
using IdentityCampaign.Application.Features.Donation.CreateDonation;
using IdentityCampaign.Application.Features.Donation.GetAllDonation;
using IdentityCampaign.Application.Features.Donation.GetDonationById;
using IdentityCampaign.Application.Features.Donation.GetDonationMe;
using IdentityCampaign.Domain.Entities;

namespace IdentityCampaign.Application.MapperProfile
{
    public class DonationProfile : Profile
    {
        public DonationProfile()
        {
            //CREATE DONATION
            CreateMap<CreateDonation, CreateDonationCommand>();
            CreateMap<CreateDonationCommand,CreateDonationResponse>();
            CreateMap<Donation, CreateDonationResponse>();
            //GET DONATION ID
            CreateMap<GetDonationByIdCommand, GetDonationByIdResponse>();
            CreateMap<Donation, GetDonationByIdResponse>();
            //GET ALL DONATION
            CreateMap<GetAllDonationCommand, GetAllDonationResponse>();
            CreateMap<Donation, GetAllDonationResponse>();
            //GET ALL DONATION USERS
            CreateMap<GetDonationMeCommand, GetDonationMeResponse>();
            CreateMap<Donation, GetDonationMeResponse>();


        }

    }
}
