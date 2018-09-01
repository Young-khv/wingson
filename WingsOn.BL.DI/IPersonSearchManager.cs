using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.BL.DI
{
    public interface IPersonSearchManager
    {
        Person GetById(int id);

        IEnumerable<Person> GetAllByFlightNumber(string flightNumber);

        IEnumerable<Person> GetAllByGender(GenderType gender);
    }
}
