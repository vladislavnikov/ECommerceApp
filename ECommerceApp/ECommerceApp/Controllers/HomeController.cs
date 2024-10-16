using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("GetInfo")]
        public IActionResult GetInfo()
        {
            return Ok("Hello world");
        }
    }
}
