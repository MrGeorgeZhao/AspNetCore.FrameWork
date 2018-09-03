using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using NLog;

namespace NetCoreFramework.WebApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoggingRequestMiddleware
    {
        private readonly RequestDelegate _next;
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();

        public LoggingRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var originalRequestBody = httpContext.Request.Body;

            var requestBodyStream = new MemoryStream();
            await httpContext.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = UriHelper.GetDisplayUrl(httpContext.Request);
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
            
            if ((!httpContext.Request.Path.ToString().ToLower().Contains("import")) && (!httpContext.Request.Path.ToString().ToLower().Contains("swagger")) && (!httpContext.Request.Path.ToString().ToLower().Contains("export"))&& (!httpContext.Request.Path.ToString().ToLower().Contains("files/template")))
                logger.Info($"{httpContext.Request.Scheme} {httpContext.Request.Host}{httpContext.Request.Path} {httpContext.Request.QueryString} {requestBodyText}");

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            httpContext.Request.Body = requestBodyStream;

            await _next(httpContext);

            httpContext.Request.Body = originalRequestBody;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoggingRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingRequestMiddleware>();
        }
    }
}
