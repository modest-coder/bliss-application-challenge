using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    public class MonitorController : ControllerBase
    {
        [HttpGet]
        [Route("health")]
        public IActionResult CheckHealth()
        {
            return Ok(new
            {
                status = "OK"
            });
        }
    }
}