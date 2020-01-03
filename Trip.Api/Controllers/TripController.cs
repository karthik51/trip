using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trip.Api.Repository;
using static Trip.Api.Helpers.Constants;
using Trip.Api.Models;

namespace Trip.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/trips")]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public TripsController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        // GET: api/v1/trips/GetAllTrips
        [HttpGet("GetAllTrips")]
        [Authorize(Roles = RoleNames.ADMIN)]              
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await _tripRepository.GetAllTrips());
        }

        // GET: api/v1/trips/GetTripsForEmployee
        [HttpGet("GetTripsForEmployee")]
        [Authorize(Roles = RoleNames.EMPLOYEE)]
        public async Task<IActionResult> GetTripsForEmployee(string username)
        {
            var trip = await _tripRepository.GetTripDriver(username);

            if (trip == null)
                return new NotFoundResult();

            return new ObjectResult(trip);
        }

        // GET: api/v1/trips/GetTripsByCustomer
        [HttpGet("GetTripsByCustomer")]
        [Authorize(Roles = RoleNames.CUSTOMER)]
        public async Task<IActionResult> GetTripsByCustomer(string name)
        {
            var trip = await _tripRepository.GetTripCustomer(name);

            if (trip == null)
                return new NotFoundResult();

            return new ObjectResult(trip);
        }

        // POST: api/v1/Trips
        [HttpPost("CreateRide")]
        [Authorize(Roles = RoleNames.CUSTOMER)]
        public async Task<IActionResult> Post([FromBody] Ride ride)
        {
            await _tripRepository.Create(ride);
            return new OkObjectResult(ride);
        }
    }
}
