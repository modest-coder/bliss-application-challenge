using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Output;
using System;

namespace BackEnd.Controllers
{
    [ApiController]
    public class MonitorController : ControllerBase
    {
        [HttpGet]
        [Route("health")]
        [Produces("application/json")]
        public IActionResult GetHealthStatus()
        {
            var random = (new Random()).Next(1, 20);
            if (random % 7 == 0) // The expected behaviour is that it throw an exception in 5% of the requests
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new GenericOutput ("Service Unavailable. Please try again later."));

            return Ok(new GenericOutput("OK"));
        }
    }
}