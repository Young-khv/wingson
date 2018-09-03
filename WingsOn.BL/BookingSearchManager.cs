using System.Collections.Generic;
using System.Linq;
using WingsOn.BL.DI;
using WingsOn.BL.Exceptions;
using WingsOn.Dal.DI;
using WingsOn.Domain;

namespace WingsOn.BL
{
    public class BookingSearchManager : IBookingSearchManager
    {
        private const string NOT_FOUND_EXCEPTION_PATTERN = "Flight with number {0} is not presented in the system";

        private readonly IRepository<Booking> _bookingRepository;

        public BookingSearchManager(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<Person> GetPassengersForFlight(string flightNumber)
        {
            var bookingsForFlight = _bookingRepository
                .GetAll()
                .Where(booking => booking.Flight.Number == flightNumber);

            if (bookingsForFlight == null || !bookingsForFlight.Any())
            {
                var exMessage = string.Format(NOT_FOUND_EXCEPTION_PATTERN, flightNumber);
                throw new DomainObjectNotFoundException(exMessage);
            }

            return bookingsForFlight
                .SelectMany(booking => booking.Passengers)
                .Distinct();
        }
    }
}
