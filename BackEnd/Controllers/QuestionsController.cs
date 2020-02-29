using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.ViewModels.Input;
using Business.Services;
using Business.Model;
using API.ViewModels;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionsService _service;
        private readonly IMapper _mapper;

        public QuestionsController(IMapper mapper, QuestionsService service)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("questions")]
        [Produces("application/json")]
        public async Task<IActionResult> GetQuestions([FromQuery]GetQuestionsInput input)
        {
            return Ok(_mapper.Map<List<PollDto>>(await _service.GetQuestions(input.limit ?? 10, input.offset ?? 0, input.filter)));
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("questions/{question_id}")]
        public async Task<IActionResult> GetQuestionById(int question_id)
        {
            var dbQuestion = await _service.GetQuestionById(question_id);
            return Ok(_mapper.Map<PollDto>(dbQuestion));
        }

        [HttpPost]
        [Route("questions")]
        [Produces("application/json")]
        public async Task<IActionResult> AddQuestion([FromBody] PollDto dto)
        {
            var newQuestion = _mapper.Map<PollDto>(await _service.AddQuestion(_mapper.Map<Poll>(dto)));
            return Created($"/questions/{newQuestion.Id}", newQuestion);
        }

        [HttpPut]
        [Produces("application/json")]
        [Route("questions/{question_id}")]
        public async Task<IActionResult> UpdateQuestion(int question_id, [FromBody]PollDto dto)
        {
            var question = _mapper.Map<PollDto>(await _service.UpdateQuestion(question_id, _mapper.Map<Poll>(dto)));
            return Created($"/questions/{question.Id}", question);
        }
    }
}
