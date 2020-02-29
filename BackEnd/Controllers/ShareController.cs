using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.ViewModels.Output;
using API.ViewModels.Input;
using Business.Services;

namespace API.Controllers
{
    [ApiController]
    public class ShareController : ControllerBase
    {
        private readonly ShareService _service;

        public ShareController(ShareService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("share")]
        [Produces("application/json")]
        public async Task<IActionResult> Share([FromBody]ShareInput input)
        {
            await _service.ShareLink(input.DestinationEmail, input.ContentUrl);
            return Ok(new GenericOutput("OK"));
        }
    }
}
