using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trip.Api.Models
{
    [BsonIgnoreExtraElements]
    public class Ride
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime RideDate { get; set; }
        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public string RideByUser { get; set; }

        public string VehicleCategoryType { get; set; }

        public string RideAcceptedByUser { get; set; }

        public bool IsRideCancelled { get; set; }

        public bool IsRideCompleted { get; set; }

        public string VehicleImage { get; set; }

        public string VehicleNumber { get; set; }
    }
}
