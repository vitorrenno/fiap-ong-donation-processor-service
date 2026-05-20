using AutoMapper;
using DonationProcessor.Application.Abstractions;
using MediatR;

namespace DonationProcessor.Application.Features.Donations.GetDonationById
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
