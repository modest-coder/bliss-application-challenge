using System.Net.Http;
using Tests.API.Dto;

namespace Tests.API
{
    public class TestHelper
    {
        public static string BaseApiUrl = "https://localhost:5001";

        public static RequestResponseDto MakeSynchronousHttpGetRequest(string route)
        {
            using (var client = new HttpClient())
            {
                var getAsync = client.GetAsync($"{BaseApiUrl}/{route}");
                getAsync.Wait();
                var response = getAsync.Result;

                var readAsync = response.Content.ReadAsStringAsync();
                readAsync.Wait();

                return new RequestResponseDto()
                {
                    StatusCode = response.StatusCode,
                    ContentType = response.Content.Headers.ContentType.MediaType,
                    Body = readAsync.Result
                };
            }
        }
    }
}
