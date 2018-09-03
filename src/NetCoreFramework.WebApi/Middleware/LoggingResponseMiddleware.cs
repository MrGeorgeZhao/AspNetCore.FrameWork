using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;

namespace NetCoreFramework.WebApi
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoggingResponseMiddleware
    {
        private readonly RequestDelegate _next;
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();

        public LoggingResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            var bodyStream = httpContext.Response.Body;

            var responseBodyStream = new MemoryStream();
            httpContext.Response.Body = responseBodyStream;

            await _next(httpContext);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();

            if ((!httpContext.Request.Path.ToString().ToLower().Contains("import")) && (!httpContext.Request.Path.ToString().ToLower().Contains("swagger")) && (!httpContext.Request.Path.ToString().ToLower().Contains("export")) && (!httpContext.Request.Path.ToString().ToLower().Contains("files/template")))
                logger.Info($"Response :{responseBody}");

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(bodyStream);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoggingResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingResponseMiddleware>();
        }
    }
}
