using Microsoft.AspNetCore.Mvc;
using projMaxPark.BL;
using System;

namespace projMaxPark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartAlgorithmController : ControllerBase
    {
        private readonly SmartAlgorithm _smartAlgorithm;

        // Inject SmartAlgorithm through the constructor
        public SmartAlgorithmController(SmartAlgorithm smartAlgorithm)
        {
            _smartAlgorithm = smartAlgorithm;
        }

        // Daily algorithm
        [HttpGet("smartAlgorithm")]
        public ActionResult<Object> Get()
        {
            try
            {
                var result = _smartAlgorithm.GetDailyAlgorithm();
                return Ok(result); // Return a 200 OK response with the result
            }
            catch (Exception ex)
            {
                // Log the exception (optional) and return a 500 Internal Server Error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
