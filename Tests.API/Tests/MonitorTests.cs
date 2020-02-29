using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Tests.API.Dto;
using System.Net;

namespace Tests.API.Tests
{
    [TestClass]
    public class MonitorTests
    {
        [TestMethod]
        public void CheckPossibleReturns()
        {
            var response = TestHelper.MakeSynchronousHttpGetRequest("health");

            Assert.AreEqual(response.ContentType, "application/json");
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.ServiceUnavailable);
            
            var body = JsonConvert.DeserializeObject<GenericResponseDto>(response.Body);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.AreEqual(body.status, "OK");
            }
            else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                Assert.AreEqual(body.status, "Service Unavailable. Please try again later.");
            }
        }
    }
}
