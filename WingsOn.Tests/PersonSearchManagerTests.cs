using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WingsOn.BL;
using WingsOn.BL.Exceptions;
using WingsOn.Dal.DI;
using WingsOn.Domain;

namespace WingsOn.Tests
{
    [TestFixture]
    public class PersonSearchManagerTests
    {
        private readonly CultureInfo _cultureInfo = new CultureInfo("nl-NL");
        private readonly PersonSearchManager _personSearchManager;

        public PersonSearchManagerTests()
        {
            var personsRepositoryMock = new Mock<IRepository<Person>>();

            personsRepositoryMock.Setup(m => m.GetAll()).Returns(GeneratePersons());

            _personSearchManager = new PersonSearchManager(personsRepositoryMock.Object);
        }

        [Test]
        public void Test_should_throw_PersonNotFoundException_for_id_not_in_the_system()
        {
            var notPresentedId = -1;

            Assert.Throws<DomainObjectNotFoundException>(() => _personSearchManager.GetById(notPresentedId));
        }

#region Repositories results generation
        private IEnumerable<Person> GeneratePersons()
        {
            return new[]
            {
                new Person
                {
                    Id = 91,
                    Address = "805-1408 Mi Rd.",
                    DateBirth = DateTime.Parse("24/09/1980", _cultureInfo),
                    Email = "egestas.a.dui@aliquet.ca",
                    Gender = GenderType.Male,
                    Name = "Kendall Velazquez"
                },
                new Person
                {
                    Id = 69,
                    Address = "P.O. Box 344, 5822 Curabitur Rd.",
                    DateBirth = DateTime.Parse("27/11/1948", _cultureInfo),
                    Email = "non.cursus.non@turpisIncondimentum.co.uk",
                    Gender = GenderType.Female,
                    Name = "Claire Stephens"
                },
                new Person
                {
                    Id = 77,
                    Address = "P.O. Box 795, 1956 Odio. Rd.",
                    DateBirth = DateTime.Parse("01/01/1940", _cultureInfo),
                    Email = "egestas.lacinia@Proinmi.com",
                    Gender = GenderType.Male,
                    Name = "Branden Johnston"
                },
                new Person
                {
                    Id = 25,
                    Address = "4320 Tempor Rd.",
                    DateBirth = DateTime.Parse("28/03/1959", _cultureInfo),
                    Email = "elit@ligula.com",
                    Gender = GenderType.Female,
                    Name = "Debra Lang"
                },
                new Person
                {
                    Id = 59,
                    Address = "977-809 Morbi Avenue",
                    DateBirth = DateTime.Parse("01/01/1958", _cultureInfo),
                    Email = "et@dictumeleifendnunc.org",
                    Gender = GenderType.Female,
                    Name = "Zenia Stout"
                }
            };
        }

        private IEnumerable<Booking> GenerateBookings()
        {
            var persons = GeneratePersons();
            var flights = GenerateFlights();
            return new[]
            {
                new Booking
                {
                    Id = 55,
                    Number = "WO-291470",
                    Customer = persons.Single(p => p.Name == "Branden Johnston"),
                    DateBooking = DateTime.Parse("03/03/2006 14:30", _cultureInfo),
                    Flight = flights.Single(f => f.Number == "BB768"),
                    Passengers = new []
                    {
                        persons.Single(p => p.Name == "Branden Johnston")
                    }
                },
                new Booking
                {
                    Id = 83,
                    Number = "WO-151277",
                    Customer = persons.Single(p => p.Name == "Debra Lang"),
                    DateBooking = DateTime.Parse("12/02/2000 12:55", _cultureInfo),
                    Flight = flights.Single(f => f.Number == "PZ696"),
                    Passengers = new []
                    {
                        persons.Single(p => p.Name == "Claire Stephens"),
                        persons.Single(p => p.Name == "Kendall Velazquez"),
                        persons.Single(p => p.Name == "Zenia Stout")
                    }
                }
            };
        }

        private IEnumerable<Flight> GenerateFlights()
        {
            return new[]
            {
                new Flight
                {
                    Id = 81,
                    Number = "PZ696",
                    DepartureAirport = new Airport(),
                    DepartureDate = DateTime.Parse("20/02/2000 17:50", _cultureInfo),
                    ArrivalAirport =  new Airport(),
                    ArrivalDate = DateTime.Parse("20/02/2000 19:00", _cultureInfo),
                    Carrier = new Airline(),
                    Price = 95.2m
                },
                new Flight
                {
                    Id = 21,
                    Number = "BB768",
                    ArrivalAirport = new Airport(),
                    ArrivalDate = DateTime.Parse("14/11/2006 21:00", _cultureInfo),
                    DepartureAirport =  new Airport(),
                    DepartureDate = DateTime.Parse("15/11/2006 01:30", _cultureInfo),
                    Carrier = new Airline(),
                    Price = 416.17m
                }
            };
        }
#endregion

    }
}
