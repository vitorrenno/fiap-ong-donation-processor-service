using AutoMapper;
using DonationProcessor.Application.Abstractions;
using DonationProcessor.Application.Messaging.Events;
using DonationProcessor.Domain.Entities;
using MassTransit;
using MediatR;

namespace DonationProcessor.Application.Features.Donations.CreateDonation
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
            var donation = new Donation(
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
