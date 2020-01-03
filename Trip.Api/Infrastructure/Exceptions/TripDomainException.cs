using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trip.Api.Infrastructure.Exceptions
{
    public class TripDomainException : Exception
    {
        public TripDomainException()
        {

        }

        public TripDomainException(string message)
            : base(message)
        { }

        public TripDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
