using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityCampaign.Application.Abstractions;
using IdentityCampaign.Application.Features.Donation.GetAllDonation;
using IdentityCampaign.Application.Features.Donation.GetDonationById;
using MediatR;

namespace IdentityCampaign.Application.Features.Donation.GetDonationMe
{
    public class GetDonationMeCommandHandler : IRequestHandler<GetDonationMeCommand, IEnumerable<GetDonationMeResponse>>
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IMapper _mapper;
        public GetDonationMeCommandHandler(IDonationRepository donationRepository, IMapper mapper)
        {
            _donationRepository = donationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDonationMeResponse>> Handle(GetDonationMeCommand request, CancellationToken cancellationToken)
        {
            var donations = await _donationRepository.GetMeAsync(request.IdUser);
            return _mapper.Map<IEnumerable<GetDonationMeResponse>>(donations);
        }
    }
}
