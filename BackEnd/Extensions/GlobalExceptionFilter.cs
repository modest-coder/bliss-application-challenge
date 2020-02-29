using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System;

namespace API.Extensions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            var exceptionType = context.Exception.GetType();
            //string message = context.Exception.Message;
            string message = "Internal Server Error";

            // That's a good "place" to handle custom exceptions
            if (exceptionType == typeof(ArgumentException))
            {
                status = HttpStatusCode.BadRequest;
            }
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                message = "Unauthorized Access";
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
                message = "A server error occurred.";
            }

            context.ExceptionHandled = true;
            //var fullMessage = message + " " + context.Exception.StackTrace;
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)status;
            // Right here is a great "place" to log errors
            response.WriteAsync(JsonConvert.SerializeObject(message));
        }
    }
}
