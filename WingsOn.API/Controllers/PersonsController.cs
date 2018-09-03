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
        public IEnumerable<Person> GetAll()
        {
            return _personSearchManager.GetAll();
        }

        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return _personSearchManager.GetById(id);
        }

        [HttpGet("male")]
        public IEnumerable<Person> GetMalePersons()
        {
            return _personSearchManager.GetAllByGender(GenderType.Male);
        }
    }
}
