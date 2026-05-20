using AutoMapper;
using IdentityCampaign.Application.Abstractions;
using IdentityCampaign.Domain.Entities;
using MediatR;

namespace IdentityCampaign.Application.Features.Donation.CreateDonation
{
    public class CreateDonationCommandHandler : IRequestHandler<CreateDonationCommand, CreateDonationResponse>
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IMapper _mapper;
        public CreateDonationCommandHandler(IDonationRepository donationRepository,  IMapper mapper)
        {
            _donationRepository = donationRepository;
            _mapper = mapper;
        }

        public async Task<CreateDonationResponse> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var donation = new Domain.Entities.Donation(request.vAmount,request.IdCampaign,request.IdUser);

            //Verifica se a campanha existe antes de doar para a mesma.
            //var campaign = await _campaignRepository.GetByIdAsync(request.IdCampaign, cancellationToken);
            //if (campaign == null)
            //    throw new KeyNotFoundException($"Campanha com ID {request.IdCampaign} não encontrado.");

            //Aplicar chamada para o service aqui
            await _donationRepository.AddAsync(donation, cancellationToken);
            return _mapper.Map<CreateDonationResponse>(donation);

        }
    }
}
