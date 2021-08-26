using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business.Helpers.Logging
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger logger)
        {
            string path = context.Request.Path.ToString();
            string message = "İstek yapılan route: " + path;
            logger.Info(message);

            context.Response.OnStarting(() =>
            {
            Task.Run(() =>
            {
                try
                {
                    logger.Info("Response kodu: " + context.Response.StatusCode + "\n");
                }
                catch { }
            });
            return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}