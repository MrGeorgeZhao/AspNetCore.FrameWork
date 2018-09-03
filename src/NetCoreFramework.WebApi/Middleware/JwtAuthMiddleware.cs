using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetCoreFramework.WebApi.Middleware;

namespace NetCoreFramework.WebApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await _next(httpContext);
            if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                httpContext.Response.StatusCode = 200;
                var newContent = "{\"errorcode\":401,\"errormessage\":\"无效token\"}";
                httpContext.Response.ContentType = "text/json";
                await httpContext.Response.WriteAsync(newContent);
            }

        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class JwtAuthMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtAuthMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtAuthMiddleware>();
    }
}

