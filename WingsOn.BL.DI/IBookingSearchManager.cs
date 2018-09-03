using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.BL.DI
{
    public interface IBookingSearchManager
    {
        IEnumerable<Person> GetPassengersForFlight(string flightNumber);
    }
}
