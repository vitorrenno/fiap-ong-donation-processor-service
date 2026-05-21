using AutoMapper;
using DonationProcessor.Application.Abstractions;
using MediatR;

namespace DonationProcessor.Application.Features.Donations.GetAllDonation
{
    public class GetAllDonationCommandHandler : IRequestHandler<GetAllDonationCommand, IEnumerable<GetAllDonationResponse>>
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IMapper _mapper;

        public GetAllDonationCommandHandler(IDonationRepository donationRepository, IMapper mapper)
        {
            _donationRepository = donationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllDonationResponse>> Handle(GetAllDonationCommand request, CancellationToken cancellationToken)
        {
            var donations = await _donationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetAllDonationResponse>>(donations);
        }
    }
}
