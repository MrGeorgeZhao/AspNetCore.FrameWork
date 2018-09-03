using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NetCoreFramework.WebApi.Handler
{
    public class GeneralExceptionFilter : IExceptionFilter
    {
        protected readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;

            var exceptionType = context.Exception.GetType();
            message = context.Exception.Message;
            status = HttpStatusCode.InternalServerError;

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            var err = message + " " + context.Exception.StackTrace;
            logger.Error(err);

            var json = "{\"errorcode\":500,\"errormessage\":\"系统错误\"}";
            response.WriteAsync(json);
        }
    }
}
