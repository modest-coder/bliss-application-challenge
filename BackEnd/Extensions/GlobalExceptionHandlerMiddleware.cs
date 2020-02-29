using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System;

namespace API.Extensions
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);

                //var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                //if (contextFeature != null)
                //{
                //    //await HandleExceptionAsync(context, ex);
                //    //logger.LogError($"Something went wrong: {contextFeature.Error}");

                //    //await context.Response.WriteAsync(new ErrorDetails()
                //    //{
                //    //    StatusCode = context.Response.StatusCode,
                //    //    Message = "Internal Server Error."
                //    //}.ToString());
                //}
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            //if (ex is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (ex is MyException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { status = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
