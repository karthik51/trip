using System.Collections.Generic;
using System.Threading.Tasks;
using Trip.Api.Data;
using Trip.Api.Helpers;
using Trip.Api.Models;

namespace Trip.Api.Repository
{
    public interface ITripRepository
    {
        Task<IEnumerable<Ride>> GetAllTrips();
        Task<List<Ride>> GetTripCustomer(string name);
        Task<List<Ride>> GetTripDriver(string name);
        Task Create(Ride ride);
    }
}