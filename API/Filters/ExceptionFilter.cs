using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Business.Exceptions;

namespace API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            // log exception message
            Console.WriteLine("Hata: {0}", context.Exception.Message);

            var statusCode = HttpStatusCode.InternalServerError;

            if (context.Exception is BadRequestException)
                statusCode = HttpStatusCode.BadRequest;

            if (context.Exception is NotAuthorizedException)
                statusCode = HttpStatusCode.Unauthorized;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;

            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                statusCode = (int)statusCode,
                stackTrace = context.Exception.StackTrace
            });

            return Task.FromResult("a");
        }
    }
}