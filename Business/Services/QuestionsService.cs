using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Business.DbConfigurations;
using System.Threading.Tasks;
using Business.Model;
using System.Linq;

namespace Business.Services
{
    public class QuestionsService
    {
        private readonly DataAccessContext _dbContext;

        public QuestionsService(DataAccessContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Poll>> GetQuestions(int limit = 10, int offset = 0, string filter = "")
        {
            #region Dynamic Predicate
            var questionsFilter = LinqKit.PredicateBuilder.New<Poll>(true);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var toLowerFilter = filter.ToLower();
                questionsFilter.And(question => question.Question.ToLower().Contains(toLowerFilter));
                questionsFilter.Or(question => question.Choices.Any(c => c.Choice.ToLower().Contains(toLowerFilter)));
            }
            #endregion Dynamic Predicate

            return await _dbContext.Polls.Where(questionsFilter).Include("Choices").Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<Poll> GetQuestionById(int questionId)
        {
            return await _dbContext.Polls.Include("Choices").FirstOrDefaultAsync(p => p.Id == questionId);
        }

        public async Task<Poll> AddQuestion(Poll poll)
        {
            _dbContext.Polls.Add(poll);
            await _dbContext.SaveChangesAsync();
            return poll;
        }

        public async Task<Poll> UpdateQuestion(int questionId, Poll newQuestion)
        {
            var dbQuestion = await GetQuestionById(questionId);
            if (dbQuestion != null)
            {
                dbQuestion.Question = newQuestion.Question;
                dbQuestion.ImageUrl = newQuestion.ImageUrl;
                dbQuestion.ThumbUrl = newQuestion.ThumbUrl;
                dbQuestion.PublishedAt = newQuestion.PublishedAt;
                await SetQuestionChoices(questionId, newQuestion);
                await _dbContext.SaveChangesAsync();
            }
            return dbQuestion;
        }

        #region Private Methods
        private async Task SetQuestionChoices(int questionId, Poll newQuestion)
        {
            _dbContext.Options.RemoveRange(_dbContext.Options.Where(opt => opt.PollId == questionId));
            if (newQuestion.Choices != null && newQuestion.Choices.Count > 0)
            {
                await _dbContext.Options.AddRangeAsync(newQuestion.Choices.Select(option => new Option
                {
                    PollId = questionId,
                    Choice = option.Choice,
                    Votes = option.Votes
                }));
            }
        }
        #endregion Private Methods
    }
}