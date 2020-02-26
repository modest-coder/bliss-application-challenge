using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System;

namespace Business.DbConfigurations
{
    public static class DbInitializerManager
    {
        public static IHost InitializeDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<DataAccessContext>())
                {
                    try
                    {
                        dbContext.Database.EnsureCreated();

                        if(dbContext.Polls.Count() < 10)
                        {
                            var questionsMockApi = GetQuestionsFromMockApi();
                            if (questionsMockApi != null && questionsMockApi.Count() > 0)
                            {
                                dbContext.Polls.AddRange(MapMockQuestionsToBusinessQuestions(questionsMockApi));
                                dbContext.SaveChanges();
                            }
                        }
                    }
                    catch { }
                }
            }

            return host;
        }

        #region Private Methods
        private static List<Poll> GetQuestionsFromMockApi()
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync("https://private-bbbe9-blissrecruitmentapi.apiary-mock.com/questions");
                task.Wait();
                return JsonConvert.DeserializeObject<List<Poll>>(task.Result);
            }
        }

        private static List<Model.Poll> MapMockQuestionsToBusinessQuestions(List<Poll> questionsMockApi)
        {
            var mappedQuestions = new List<Model.Poll>();
            foreach(var question in questionsMockApi)
            {
                var newQuestion = new Model.Poll()
                {
                    Id = question.id,
                    Question = question.question,
                    ImageUrl = question.image_url,
                    ThumbUrl = question.thumb_url,
                    PublishedAt = question.published_at
                };

                foreach(var choice in question.choices)
                {
                    newQuestion.Choices.Add(new Model.Option()
                    {
                        Choice = choice.choice,
                        Votes = choice.votes
                    });
                }
                mappedQuestions.Add(newQuestion);
            }
            return mappedQuestions;
        }
        #endregion Private Methods

        #region Private Classes
        private class Poll
        {
            public int id { get; set; }
            public string question { get; set; }
            public string image_url { get; set; }
            public string thumb_url { get; set; }
            public DateTime published_at { get; set; }
            public List<Option> choices { get; set; } = new List<Option>();
        }

        private class Option
        {
            public string choice { get; set; }
            public int votes { get; set; }
        }
        #endregion Private Classes
    }
}
