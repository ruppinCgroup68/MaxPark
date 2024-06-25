using MaxPark.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaxPark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // read today Events
        [HttpGet("events")]
        public IEnumerable<Event> readEvents()
        {
            Event ev = new Event();
            return ev.readEvents();
        }

        // POST event
        [HttpPost("event")]
        public int Post([FromBody] Event ev)
        {
            Event e= new Event();
            return e.addNewEvent(ev);
        }

        //// PUT api/<EventsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //Delete Event
        [HttpDelete("event")]
        public IActionResult DeleteUser(int eventId)
        {
            Event ev = new Event();
            return (ev.deleteEvent(eventId) != 0) ? Ok("הזמנה נמחקה בהצלחה") : BadRequest("פעולת המחיקה נכשלה...");
        }


    }
}
