using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business.Model.snake_case_model;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace Tests.API.Tests
{
    [TestClass]
    public class QuestionsTests
    {
        #region Get Questions
        [TestMethod]
        public void GetQuestions_ShouldReturnOkStatusCode()
        {
            var response = TestHelper.MakeSynchronousHttpGetRequest("questions");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);

            var questions = JsonConvert.DeserializeObject<List<poll>>(response.Body);
            Assert.IsTrue(questions.Count >= 10);
        }
        #endregion Get Questions

        #region Get Question By Id
        [TestMethod]
        public void GetQuestionById_ShouldReturnOkStatusCode()
        {
            var questionId = 5;
            var response = TestHelper.MakeSynchronousHttpGetRequest($"questions/{questionId}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);
            var question = JsonConvert.DeserializeObject<poll>(response.Body);
            Assert.AreEqual(questionId, question.id);
        }

        [TestMethod]
        public void GetQuestionById_ShouldReturnBadRequestStatusCode()
        {
            var questionId = 0;
            var response = TestHelper.MakeSynchronousHttpGetRequest($"questions/{questionId}");

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual($"It wasn't possible to find the entity for the id: {questionId}", JsonConvert.DeserializeObject(response.Body));
        }
        #endregion Get Question By Id

        #region Add Question
        [TestMethod]
        public void AddQuestion_WithQuestionFieldNull_ShouldReturnBadRequestStatusCode()
        {
            var question = TestHelper.GetMockedPoll();
            question.question = null;

            var json = JsonConvert.SerializeObject(question);
            var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Post, $"questions", json);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);

            var result = JsonConvert.DeserializeObject(response.Body);
            var errorMessage = ((JValue)((JContainer)((JContainer)((JContainer)((JContainer)((JContainer)result).Last).First).First).First).First).Value;
            Assert.AreEqual("The Question field is required.", errorMessage);
        }

        [TestMethod]
        public void AddQuestion_WithChoiceFieldNull_ShouldReturnBadRequestStatusCode()
        {
            var question = TestHelper.GetMockedPoll();
            question.choices[0].choice = null;

            var json = JsonConvert.SerializeObject(question);
            var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Post, $"questions", json);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);

            var result = JsonConvert.DeserializeObject(response.Body);
            var errorMessage = ((JValue)((JContainer)((JContainer)((JContainer)((JContainer)((JContainer)result).Last).First).First).First).First).Value;
            Assert.AreEqual("The Choice field is required.", errorMessage);
        }

        //[TestMethod]
        //public void AddQuestion_WithRepeatedId_ShouldReturnBadRequestStatusCode()
        //{
        //    var question = TestHelper.GetMockedPoll();
        //    question.id = 1;

        //    var json = JsonConvert.SerializeObject(question);
        //    var response = TestHelper.MakeSynchronousHttpPostRequest($"questions", json);

        //    Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        //    Assert.AreEqual(response.ContentType, "application/json");

        //    var result = JsonConvert.DeserializeObject(response.Body);
        //    var errorMessage = ((JValue)((JContainer)((JContainer)((JContainer)((JContainer)((JContainer)result).Last).First).First).First).First).Value;
        //    Assert.AreEqual(errorMessage, "The Choice field is required.");
        //}

        [TestMethod]
        public void AddQuestion_ShouldReturnOkStatusCode()
        {
            var json = JsonConvert.SerializeObject(TestHelper.GetMockedPoll());
            var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Post, $"questions", json);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);
            var newQuestion = JsonConvert.DeserializeObject<poll>(response.Body);
            Assert.AreEqual($"/questions/{newQuestion.id}", response.Location);
        }
        #endregion Add Question

        #region Update Question
        [TestMethod]
        public void UpdateQuestion_WithQuestionFieldNull_ShouldReturnBadRequestStatusCode()
        {
            var question = TestHelper.GetMockedPoll();
            question.question = null;
            var questionId = 3;

            var json = JsonConvert.SerializeObject(question);
            var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Put, $"questions/{questionId}", json);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);

            var result = JsonConvert.DeserializeObject(response.Body);
            var errorMessage = ((JValue)((JContainer)((JContainer)((JContainer)((JContainer)((JContainer)result).Last).First).First).First).First).Value;
            Assert.AreEqual("The Question field is required.", errorMessage);
        }

        [TestMethod]
        public void UpdateQuestion_WithChoiceFieldNull_ShouldReturnBadRequestStatusCode()
        {
            var question = TestHelper.GetMockedPoll();
            question.choices[0].choice = null;
            var questionId = 3;

            var json = JsonConvert.SerializeObject(question);
            var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Put, $"questions/{questionId}", json);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);

            var result = JsonConvert.DeserializeObject(response.Body);
            var errorMessage = ((JValue)((JContainer)((JContainer)((JContainer)((JContainer)((JContainer)result).Last).First).First).First).First).Value;
            Assert.AreEqual("The Choice field is required.", errorMessage);
        }

        [TestMethod]
        public void UpdateQuestion_ShouldReturnOkStatusCode()
        {
            var questionId = 3;
            var json = JsonConvert.SerializeObject(TestHelper.GetMockedPoll());
            var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Put, $"questions/{questionId}", json);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);
            var newQuestion = JsonConvert.DeserializeObject<poll>(response.Body);
            Assert.AreEqual($"/questions/{newQuestion.id}", response.Location);
        }
        #endregion Update Question
    }
}
