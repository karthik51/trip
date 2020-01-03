using MongoDB.Driver;
using Trip.Api.Helpers;
using Trip.Api.Models;

namespace Trip.Api.Data
{
    public interface ITripContext
    {
        IMongoCollection<Ride> Rides { get; }
    }
}