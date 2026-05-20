using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityCampaign.Application.Abstractions;
using IdentityCampaign.Application.Features.Donation.CreateDonation;
using MediatR;

namespace IdentityCampaign.Application.Features.Donation.GetDonationById
{
    public class GetDonationByIdCommandHandler : IRequestHandler<GetDonationByIdCommand, GetDonationByIdResponse>
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IMapper _mapper;
        public GetDonationByIdCommandHandler(IDonationRepository donationRepository, IMapper mapper)
        {
            _donationRepository = donationRepository;
            _mapper = mapper;
        }

        public async Task<GetDonationByIdResponse> Handle(GetDonationByIdCommand request, CancellationToken cancellationToken)
        {
            var donation = await _donationRepository.GetByIdAsync(request.Id);

            if (donation == null)
                throw new KeyNotFoundException($"Donation Id: {request.Id} not found");

            return _mapper.Map<GetDonationByIdResponse>(donation);

        }
    }
}
