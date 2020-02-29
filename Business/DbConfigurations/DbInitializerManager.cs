using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;

using Business.Model.snake_case_model;
using Business.Model;

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
        private static List<poll> GetQuestionsFromMockApi()
        {
            using (var client = new HttpClient())
            {
                var task = client.GetStringAsync("https://private-bbbe9-blissrecruitmentapi.apiary-mock.com/questions");
                task.Wait();
                return JsonConvert.DeserializeObject<List<poll>>(task.Result);
            }
        }

        private static List<Poll> MapMockQuestionsToBusinessQuestions(List<poll> questionsMockApi)
        {
            var mappedQuestions = new List<Poll>();
            foreach(var question in questionsMockApi)
            {
                var newQuestion = new Poll()
                {
                    Id = question.id,
                    Question = question.question,
                    ImageUrl = question.image_url,
                    ThumbUrl = question.thumb_url,
                    PublishedAt = question.published_at
                };

                foreach(var choice in question.choices)
                {
                    newQuestion.Choices.Add(new Option()
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
    }
}
