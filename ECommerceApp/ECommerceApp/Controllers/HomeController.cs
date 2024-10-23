using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> _logger)
        {
            this.logger = _logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getInfo")]
        public IActionResult GetInfo()
        {
            this.logger.LogInformation("Handling GetInfo request");

            try
            {
                return Ok("Information retrieved successfully.");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error occurred while processing GetInfo request");
                return StatusCode(500, "An error occurred.");
            }
        }
    }
}
