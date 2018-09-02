using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WingsOn.BL.DI;
using WingsOn.Domain;

namespace WingsOn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonSearchManager _personSearchManager;

        public PersonsController(IPersonSearchManager personSearchManager)
        {
            _personSearchManager = personSearchManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            return new ActionResult<IEnumerable<Person>>(_personSearchManager.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            return _personSearchManager.GetById(id);
        }

        [HttpGet("flight/{flightNumber}")]
        public ActionResult<IEnumerable<Person>> GetByFlight(string flightNumber)
        {
            return new ActionResult<IEnumerable<Person>>(_personSearchManager.GetAllByFlightNumber(flightNumber));
        }

        [HttpGet("male")]
        public ActionResult<IEnumerable<Person>> GetMalePersons()
        {
            return new ActionResult<IEnumerable<Person>>(_personSearchManager.GetAllByGender(GenderType.Male));
        }
    }
}
