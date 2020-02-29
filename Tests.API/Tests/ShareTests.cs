using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Tests.API.Dto;
using System.Net;

namespace Tests.API.Tests
{
    [TestClass]
    public class ShareTests
    {
        [TestMethod]
        public void TestShareRoute_WithDestinationEmailNull_ShouldReturnBadRequestStatusCode()
        {
            var input = TestHelper.GetMockedShareInput();
            input.destination_email = null;

            var json = JsonConvert.SerializeObject(input);
            var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Post, $"share", json);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);

            var result = JsonConvert.DeserializeObject(response.Body);
            var errorMessage = ((JValue)((JContainer)((JContainer)((JContainer)((JContainer)((JContainer)result).Last).First).First).First).First).Value;
            Assert.AreEqual("The DestinationEmail field is required.", errorMessage);
        }

        [TestMethod]
        public void TestShareRoute_WithInvalidDestinationEmail_ShouldReturnBadRequestStatusCode()
        {
            var input = TestHelper.GetMockedShareInput();
            input.destination_email = "some-invalid-email";

            var json = JsonConvert.SerializeObject(input);
            var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Post, $"share", json);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);

            var result = JsonConvert.DeserializeObject(response.Body);
            var errorMessage = ((JValue)((JContainer)((JContainer)((JContainer)((JContainer)((JContainer)result).Last).First).First).First).First).Value;
            Assert.AreEqual("The DestinationEmail field is not a valid e-mail address.", errorMessage);
        }

        [TestMethod]
        public void TestShareRoute_WithContentUrlNull_ShouldReturnBadRequestStatusCode()
        {
            var input = TestHelper.GetMockedShareInput();
            input.content_url = null;

            var json = JsonConvert.SerializeObject(input);
            var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Post, $"share", json);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/json", response.ContentType);

            var result = JsonConvert.DeserializeObject(response.Body);
            var errorMessage = ((JValue)((JContainer)((JContainer)((JContainer)((JContainer)((JContainer)result).Last).First).First).First).First).Value;
            Assert.AreEqual("The ContentUrl field is required.", errorMessage);
        }

        [TestMethod]
        public void TestShareRoute_ShouldReturnOkStatusCode()
        {
            if (TestHelper.BaseApiUrl.Contains("500")) // Sending e-mail locally
            {
                var json = JsonConvert.SerializeObject(TestHelper.GetMockedShareInput());
                var response = TestHelper.MakeSynchronousHttpRequestWithBody(TestHelper.HttpRequestType.Post, $"share", json);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual("application/json", response.ContentType);
                var result = JsonConvert.DeserializeObject<GenericResponseDto>(response.Body);
                Assert.AreEqual("OK", result.status);
            }
        }
    }
}
