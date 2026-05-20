using AutoMapper;
using DonationProcessor.Application.DTOs.Donation;
using DonationProcessor.Application.Features.Donations.CreateDonation;
using DonationProcessor.Application.Features.Donations.GetAllDonation;
using DonationProcessor.Application.Features.Donations.GetDonationById;
using DonationProcessor.Application.Features.Donations.GetDonationMe;
using DonationProcessor.Domain.Entities;

namespace DonationProcessor.Application.MapperProfile
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
