using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.ViewModels.Input;

namespace API.Controllers
{
    public class ShareController : ControllerBase
    {
        [HttpPost]
        [Route("share")]
        public async Task<IActionResult> Share([FromBody]ShareInput input)
        {
            return Ok(new { status = "OK" });
        }
    }
}
