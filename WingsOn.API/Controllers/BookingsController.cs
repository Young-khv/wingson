using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WingsOn.BL.DI;
using WingsOn.Domain;

namespace WingsOn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingSearchManager _bookingSearchManager;

        public BookingsController(IBookingSearchManager bookingSearchManager)
        {
            _bookingSearchManager = bookingSearchManager;
        }

        [HttpGet("passengers")]
        public IEnumerable<Person> GetPassengersForFlight([FromQuery] string flightNumber)
        {
            return _bookingSearchManager.GetPassengersForFlight(flightNumber);
        }
    }
}