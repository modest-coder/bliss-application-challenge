using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels.Output;
using Business.Services;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly PollsService _service;
        private readonly IMapper _mapper;

        public PollsController(IMapper mapper, PollsService service)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("questions")]
        public List<PollDto> GetPolls()
        {
            return _mapper.Map<List<PollDto>>(_service.GetPolls());
        }
    }
}
