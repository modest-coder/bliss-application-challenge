using Business.Model.snake_case_model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Tests.API.Dto;
using System.Text;
using System.Net;
using System;

namespace Tests.API
{
    public class TestHelper
    {
        public static string BaseApiUrl = "https://localhost:5001";

        public enum HttpRequestType
        {
            Post,
            Put
        }

        public static poll GetMockedPoll()
        {
            return new poll()
            {
                question = "What's the meaning of life the universe and everything?",
                image_url = "First link",
                thumb_url = "Second link",
                published_at = DateTime.UtcNow,
                choices = new List<option>()
                {
                    new option() { choice = "I don't know", votes = 256 },
                    new option() { choice = "Do you know?", votes = 512 },
                    new option() { choice = "I think that is...", votes = 1024 },
                    new option() { choice = "42", votes = 2048 }
                }
            };
        }

        public static ShareInputDto GetMockedShareInput()
        {
            return new ShareInputDto()
            {
                destination_email = "some.email@here.com",
                content_url = @"Do you know what's the meaning of life the universe and everything?\n
                                So, just to you know, the answer is 42.\n\n
                                Now you just have to find the question."
            };
        }

        public static RequestResponseDto MakeSynchronousHttpGetRequest(string route)
        {
            using (var client = new HttpClient())
            {
                var getAsync = client.GetAsync($"{BaseApiUrl}/{route}");
                getAsync.Wait();

                return new RequestResponseDto()
                {
                    StatusCode = getAsync.Result.StatusCode,
                    ContentType = getAsync.Result.Content.Headers.ContentType.MediaType,
                    Body = getAsync.Result.Content.ReadAsStringAsync().Result
                };
            }
        }

        public static RequestResponseDto MakeSynchronousHttpRequestWithBody(HttpRequestType requestType, string route, string input)
        {
            using (var client = new HttpClient())
            {
                var data = new StringContent(input, Encoding.UTF8, "application/json");

                var responseAsync = Task.FromResult(new HttpResponseMessage());
                if(requestType == HttpRequestType.Post)
                    responseAsync = client.PostAsync($"{BaseApiUrl}/{route}", data);
                else if(requestType == HttpRequestType.Put)
                    responseAsync = client.PutAsync($"{BaseApiUrl}/{route}", data);

                responseAsync.Wait();

                var response = new RequestResponseDto()
                {
                    ContentType = responseAsync.Result.Content.Headers.ContentType.MediaType,
                    Body = responseAsync.Result.Content.ReadAsStringAsync().Result,
                    StatusCode = responseAsync.Result.StatusCode
                };

                if(response.StatusCode == HttpStatusCode.Created)
                    response.Location = responseAsync.Result.Headers.Location.OriginalString;

                return response;
            }
        }
    }
}
