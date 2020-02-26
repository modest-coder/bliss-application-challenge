using System.Collections.Generic;
using Business.Model;
using System;

namespace Business.Services
{
    public class PollsService
    {
        public List<Poll> GetPolls()
        {
            return new List<Poll>()
            {
                new Poll()
                {
                    Id = 1,
                    Question = "Favourite programming language?",
                    ImageUrl = "https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)",
                    ThumbUrl = "https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)",
                    PublishedAt = Convert.ToDateTime("2015-08-05T08:40:51.620Z"),
                    Choices = new List<Option>()
                    {
                        new Option() { Choice = "Swift", Votes = 2048 },
                        new Option() { Choice = "Python", Votes = 1024 },
                        new Option() { Choice = "Objective-C", Votes = 512 },
                        new Option() { Choice = "Ruby", Votes = 256 }
                    }
                }
            };
        }
    }
}
