using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.ViewModels.Output;
using Business.Services;
using Business.Model;
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
        public async Task<IActionResult> GetQuestions()
        {
            return Ok(_mapper.Map<List<PollDto>>(await _service.GetQuestions()));
        }

        [HttpGet]
        [Route("questions/{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var dbQuestion = await _service.GetQuestionById(id);
            if (dbQuestion == null)
            {
                return BadRequest($"It wasn't possible to find the entity for the id: {id}");
            }

            return Ok(_mapper.Map<PollDto>(dbQuestion));
        }

        [HttpPost]
        [Route("questions")]
        public async Task<IActionResult> AddQuestion(PollDto dto)
        {
            return Ok(_mapper.Map<PollDto>(await _service.AddQuestion(_mapper.Map<Poll>(dto))));
        }

        [HttpPut]
        [Route("questions/{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody]PollDto dto)
        {
            var result = await _service.UpdateQuestion(id, _mapper.Map<Poll>(dto));
            if(result == null)
            {
                return BadRequest($"It wasn't possible to find the entity for the id: {id}");
            }
            return Ok(_mapper.Map<PollDto>(result));
        }
    }
}
