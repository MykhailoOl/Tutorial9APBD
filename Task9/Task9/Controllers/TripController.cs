using Microsoft.AspNetCore.Mvc;
using task9.Repositories;
using task9.DTO;
using task9.Entities;

namespace task9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;
        private readonly IClientTripRepository _clientTripRepository;

        public TripsController(ITripRepository tripRepository, IClientTripRepository clientTripRepository)
        {
            _tripRepository = tripRepository;
            _clientTripRepository = clientTripRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var trips = await _tripRepository.GetTripsAsync(page, pageSize);
            var totalTrips = await _tripRepository.GetTotalTripsCountAsync();
            var totalPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

            return Ok(new
            {
                pageNum = page,
                pageSize,
                allPages = totalPages,
                trips
            });
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<ActionResult> AddClientToTrip(AddClientDTO addClientDto, int idTrip)
        {
            var client = await _clientTripRepository.GetClientByPeselAsync(addClientDto.Pesel);
            if (client == null)
            {
                return BadRequest("Pesel doesn't exist");
            }

            var tripClient = await _clientTripRepository.GetClientTripAsync(client.IdClient, idTrip);
            if (tripClient != null)
            {
                return BadRequest("Already registered");
            }

            var trip = await _tripRepository.GetTripByIdAsync(idTrip);
            if (trip == null)
            {
                return BadRequest("ID doesn't exist");
            }

            if (trip.DateFrom < DateTime.Now)
            {
                return BadRequest("Already occured");
            }

            var newClientTrip = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = trip.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = addClientDto.PaymentDate
            };

            await _clientTripRepository.AddClientTripAsync(newClientTrip);

            return Ok();
        }
    }
}
