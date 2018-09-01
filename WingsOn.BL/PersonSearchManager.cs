using System.Collections.Generic;
using System.Linq;
using WingsOn.BL.DI;
using WingsOn.Dal.DI;
using WingsOn.Domain;

namespace WingsOn.BL
{
    public class PersonSearchManager : IPersonSearchManager
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Flight> _flightRepository;

        public PersonSearchManager(IRepository<Person> personRepository,
                                   IRepository<Booking> bookingRepository,
                                   IRepository<Flight> flightRepository)
        {
            _personRepository = personRepository;
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
        }

        public Person GetById(int id)
        {
            var person = _personRepository.Get(id);

            if (person == null)
            {
                var exMessage = $"Person with id {id} is not presented in the system";
                throw new PersonNotFoundException(exMessage);
            }

            return person;
        }

        public IEnumerable<Person> GetAllByFlightNumber(string flightNumber)
        {
            var bookingsForFlight = _bookingRepository
                .GetAll()
                .Where(booking => booking.Flight.Number == flightNumber);

            return bookingsForFlight
                .SelectMany(booking => booking.Passengers)
                .Distinct();
        }

        public IEnumerable<Person> GetAllByGender(GenderType gender)
        {
            return _personRepository
                        .GetAll()
                        .Where(person => person.Gender == gender);
        }
    }
}
