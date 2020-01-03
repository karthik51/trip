using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Trip.Api.Data;
using Trip.Api.Helpers;
using Trip.Api.Models;

namespace Trip.Api.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly ITripContext _context;

        public TripRepository(ITripContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ride>> GetAllTrips()
        {
            return await _context
                            .Rides
                            .Find(_ => true)
                            .ToListAsync();
        }

        public Task<List<Ride>> GetTripCustomer(string username)
        {
            FilterDefinition<Ride> filter = Builders<Ride>.Filter.Eq(m => m.RideByUser, username);

            return _context
                    .Rides
                    .Find(filter)
                    .ToListAsync();
        }

        public Task<List<Ride>> GetTripDriver(string username)
        {
            FilterDefinition<Ride> filter = Builders<Ride>.Filter.Eq(m => m.RideAcceptedByUser, username);

            return _context
                    .Rides
                    .Find(filter)
                    .ToListAsync();
        }

        public async Task Create(Ride ride)
        {
            await _context.Rides.InsertOneAsync(ride);
        }
    }
}