using Microsoft.AspNetCore.Mvc;
using projMaxPark.BL;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaxPark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        //return Users List
        [HttpGet("users")]
        public IEnumerable<User> Get()
        {
            Admin admin = new Admin();
            return admin.getAllUsers();
        }
        //Insert New User
        [HttpPost("user")]
        public int Post([FromBody] User user)
        {
            Admin admin = new Admin();
            return admin.insertUser(user);
        }

        //Delete User
        //[HttpDelete("user")]
        //public IActionResult DeleteUser(int userId)
        //{
        //    Admin admin = new Admin();
        //    return (admin.deleteUser(userId) != 0) ? Ok("הזמנה נמחקה בהצלחה") : BadRequest("פעולת המחיקה נכשלה...");
        //}

        //Update users 
        [HttpPut("user")]
        public User UpdateUser([FromBody] User user)
        {
            Admin admin = new Admin();
            return admin.updateUser(user);
        }



        //New Prking Mark 
        [HttpPost("addMark")]
        public int PostNewMark([FromBody] Mark[] marks)
        {
            Admin admin = new Admin();
            int res = 0;
            foreach (var mark in marks)
            {
                res = admin.insertMark(mark.MarkName, mark.MarkName_Block);
            }
            return res;
        }

        //Delete all parking marks (together)
        [HttpDelete("deleteParkingMarks")]
        public IActionResult Delete()
        {
            try
            {
                Admin admin = new Admin();
                int numEffected = admin.deleteParkingMarks();
                return Ok(numEffected + " slots deleted succsessfuly");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
