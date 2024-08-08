using Microsoft.AspNetCore.Mvc;
using projMaxPark.BL;
using projMaxPark.DAL;
using System.Net;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaxPark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        //User LogIn   
        [HttpPost]
        [Route("LogIn")]
        public Object LogIn([FromBody] User user)
        {
            return user.Login();
        }

        [HttpPost]
        [Route("notificationCode")]
        public IActionResult NotificationCode([FromBody] NotificationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.NotificationCode))
            {
                return BadRequest("Invalid request.");
            }

            try
            {
                User user = new User();
                user.SaveNotificationCode(request.UserId, request.NotificationCode);
                return Ok("Notification code saved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, "An error occurred while saving the notification code.");
            }
        }

        // Create a DTO class for the request
        public class NotificationRequest
        {
            public int UserId { get; set; }
            public string NotificationCode { get; set; }
        }
        //User's Reservation List
        [HttpGet]
        [Route("getUserById/{userId}")]
        public Object getUserById(int userId)
        {
            User res = new User();

            return res.GetUserById(userId);
        }

        //User's Reservation List
        [HttpGet]
        [Route("reservationList")]
        public List<Reservation> getReservetionList(int userId)
        {
            User dbs = new User();
            return dbs.getReservationList(userId);
        }



        //get user details
        //update profile details
        //add pictures "העלאת קבצים לשרת" - Class 
        [HttpPut("updateUserDetails")]
        public IActionResult UpdateUserDetails([FromBody] User user)
        {
            int result = user.UpdateUserDetails();
            if (result > 0)
                return Ok("Update successful");
            else
                return NotFound("User not found");
        }
    }
}

