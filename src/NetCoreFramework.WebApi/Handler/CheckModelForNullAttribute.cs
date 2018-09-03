using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NetCoreFramework.WebApi.Handler
{
    public class CheckModelForNullAttribute : ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Count.Equals(0) || context.ActionArguments.Any(k => k.Value == null))
            {
                context.Result = new BadRequestObjectResult("The request argument cannot be null");
            }
        }
    }
}
