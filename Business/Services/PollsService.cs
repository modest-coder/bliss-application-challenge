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

        public async Task<List<Poll>> GetPolls()
        {
            return await _dbContext.Polls.Include("Options").ToListAsync();
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
    }
}
