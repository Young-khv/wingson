using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Domain;

namespace WingsOn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return new[] { new Person() };
        }

        [HttpGet("{id}")]
        public Person Get(int id)
        {
            return new Person();
        }
    }
}
