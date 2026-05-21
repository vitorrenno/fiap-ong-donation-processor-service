using AutoMapper;
using DonationProcessor.Application.Abstractions;
using MediatR;

namespace DonationProcessor.Application.Features.Donations.GetDonationMe
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
