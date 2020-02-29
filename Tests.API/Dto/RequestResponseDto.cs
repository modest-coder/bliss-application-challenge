using System.Net;

namespace Tests.API.Dto
{
    public class RequestResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
        public string Location { get; set; }
        public string Body { get; set; }
    }
}
