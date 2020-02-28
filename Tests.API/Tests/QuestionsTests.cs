using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business.Model.snake_case_model;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

namespace Tests.API.Tests
{
    [TestClass]
    public class QuestionsTests
    {
        [TestMethod]
        public void ShouldGetAllQuestions()
        {
            var response = TestHelper.MakeSynchronousHttpGetRequest("questions");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.AreEqual(response.ContentType, "application/json");
                var questions = JsonConvert.DeserializeObject<List<poll>>(response.Body);

                Assert.IsTrue(questions.Count >= 10);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
