using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Business.DbConfigurations;
using System.Threading.Tasks;
using Business.Model;
using System.Linq;

namespace Business.Services
{
    public class PollsService
    {
        private readonly DataAccessContext _dbContext;

        public PollsService(DataAccessContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Poll>> GetQuestions()
        {
            return await _dbContext.Polls.Include("Choices").ToListAsync();
            //return new List<Poll>()
            //{
            //    new Poll()
            //    {
            //        Id = 1,
            //        Question = "Favourite programming language?",
            //        ImageUrl = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)",
            //        ThumbUrl = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)",
            //        PublishedAt = Convert.ToDateTime("2015-08-05T08:40:51.620Z"),
            //        Choices = new List<Option>()
            //        {
            //            new Option() { Choice = "Swift", Votes = 2048 },
            //            new Option() { Choice = "Python", Votes = 1024 },
            //            new Option() { Choice = "Objective-C", Votes = 512 },
            //            new Option() { Choice = "Ruby", Votes = 256 }
            //        }
            //    }
            //};
        }

        public async Task<Poll> GetQuestionById(int questionId)
        {
            return await _dbContext.Polls.Include("Choices").FirstOrDefaultAsync(p => p.Id == questionId);
        }

        public async Task<Poll> AddPoll(Poll poll)
        {
            _dbContext.Polls.Add(poll);
            await _dbContext.SaveChangesAsync();
            return poll;
        }

        public async Task<Poll> UpdatePoll(int questionId, Poll newQuestion)
        {
            var dbQuestion = await _dbContext.Polls.FirstOrDefaultAsync(p => p.Id == questionId);
            if(dbQuestion != null)
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