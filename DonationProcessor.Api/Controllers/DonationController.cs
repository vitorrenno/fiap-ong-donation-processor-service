using AutoMapper;
using IdentityCampaign.Application.DTOs.Donation;
using IdentityCampaign.Application.Features.Donation.CreateDonation;
using IdentityCampaign.Application.Features.Donation.GetAllDonation;
using IdentityCampaign.Application.Features.Donation.GetDonationById;
using IdentityCampaign.Application.Features.Donation.GetDonationMe;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCampaign.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DonationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonation request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateDonationCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);
            return Created(string.Empty, response);
        }
        [HttpGet("{id}")]   
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<GetDonationByIdCommand>(id);
            var donation = await _mediator.Send(command);

            if (donation is null)
                return NotFound();

            return Ok(donation);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllDonationCommand();
            var campaigns = await _mediator.Send(query);
            return Ok(campaigns);
        }
        [HttpGet("GetDonationByUser/{idUser}")]
        public async Task<IActionResult> GetDonationByUser(Guid idUser, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<GetDonationMeCommand>(idUser);
            var donations = await _mediator.Send(command);

            if (donations is null)
                return NotFound();

            return Ok(donations);
        }

    }
}
