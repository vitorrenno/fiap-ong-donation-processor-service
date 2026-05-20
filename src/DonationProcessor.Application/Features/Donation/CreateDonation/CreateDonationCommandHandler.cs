using AutoMapper;
using IdentityCampaign.Application.Messaging.Events;
using IdentityCampaign.Application.Abstractions;
using IdentityCampaign.Application.Features.Donation.CreateDonation;
using MassTransit;
using MediatR;

namespace DonationProcessor.Application.Features.Donation.CreateDonation
{
    public class CreateDonationCommandHandler : IRequestHandler<CreateDonationCommand, CreateDonationResponse>
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateDonationCommandHandler(IDonationRepository donationRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _donationRepository = donationRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<CreateDonationResponse> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {
            var donation = new IdentityCampaign.Domain.Entities.Donation(
                request.vAmount,
                request.IdCampaign,
                request.IdUser);

            //Aplicar chamada para o service aqui
            await _donationRepository.AddAsync(donation, cancellationToken);

            Console.WriteLine("PUBLICANDO EVENTO");

            //Publica evento no RabbitMQ
            await _publishEndpoint.Publish(
                new DonationReceivedEvent(
                    donation.Id,
                    donation.IdCampaign,
                    donation.vAmount,
                    DateTime.UtcNow),
                cancellationToken);

            Console.WriteLine("EVENTO PUBLICADO");

            return _mapper.Map<CreateDonationResponse>(donation);
        }
    }
}
