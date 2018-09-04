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

            personsRepositoryMock.Setup(m => m.Get(It.IsAny<int>()))
                .Returns((int id) => GeneratePersons().FirstOrDefault(person => person.Id == id));

            _personSearchManager = new PersonSearchManager(personsRepositoryMock.Object);
        }

        [Test]
        public void Test_should_throw_DomainObjectNotFoundException_for_id_not_in_the_system()
        {
            var notPresentedId = -1;

            Assert.Throws<DomainObjectNotFoundException>(() => _personSearchManager.GetById(notPresentedId));
        }

        [Test]
        public void Test_should_return_single_person_with_specified_id()
        {
            var id = 91;

            var person = _personSearchManager.GetById(id);

            Assert.NotNull(person);
            Assert.AreEqual(id, person.Id);
        }

        [Test]
        public void Test_should_return_only_males()
        {
            var malesCount = GeneratePersons()
                .Count(person => person.Gender == GenderType.Male);

            var males = _personSearchManager.GetAllByGender(GenderType.Male).ToArray();

            Assert.NotNull(males);
            Assert.IsNotEmpty(males);
            Assert.AreEqual(malesCount, males.Length);
            Assert.True(males.All(person => person.Gender == GenderType.Male));
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
#endregion

    }
}
