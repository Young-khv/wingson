using System.Collections.Generic;
using System.Linq;
using WingsOn.BL.DI;
using WingsOn.BL.Exceptions;
using WingsOn.Dal.DI;
using WingsOn.Domain;

namespace WingsOn.BL
{
    public class PersonSearchManager : IPersonSearchManager
    {
        private const string NOT_FOUND_EXCEPTION_PATTERN = "Person with id {0} is not presented in the system";

        private readonly IRepository<Person> _personRepository;

        public PersonSearchManager(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public Person GetById(int id)
        {
            var person = _personRepository.Get(id);

            if (person == null)
            {
                var exMessage = string.Format(NOT_FOUND_EXCEPTION_PATTERN, id);
                throw new DomainObjectNotFoundException(exMessage);
            }

            return person;
        }

        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetAll();
        }

        public IEnumerable<Person> GetAllByGender(GenderType gender)
        {
            return _personRepository
                        .GetAll()
                        .Where(person => person.Gender == gender);
        }
    }
}
