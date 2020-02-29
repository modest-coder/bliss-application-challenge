using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Tests.API.Dto;
using System.Net;

namespace Tests.API.Tests
{
    [TestClass]
    public class ShareTests
    {
        [TestMethod]
        public void TestShareRoute_ShouldReturnOkStatusCode()
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
