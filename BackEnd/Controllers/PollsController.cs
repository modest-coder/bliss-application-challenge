using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.ViewModels.Output;
using API.ViewModels.Input;
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
        public async Task<IActionResult> GetQuestions([FromQuery]GetQuestionsInput input)
        {
            return Ok(_mapper.Map<List<PollOutput>>(await _service.GetQuestions(input.limit ?? 10, input.offset ?? 0, input.filter)));
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

            return Ok(_mapper.Map<PollOutput>(dbQuestion));
        }

        [HttpPost]
        [Route("questions")]
        public async Task<IActionResult> AddQuestion(PollOutput dto)
        {
            return Ok(_mapper.Map<PollOutput>(await _service.AddQuestion(_mapper.Map<Poll>(dto))));
        }

        [HttpPut]
        [Route("questions/{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody]PollOutput dto)
        {
            var result = await _service.UpdateQuestion(id, _mapper.Map<Poll>(dto));
            if(result == null)
            {
                return BadRequest($"It wasn't possible to find the entity for the id: {id}");
            }
            return Ok(_mapper.Map<PollOutput>(result));
        }
    }
}
