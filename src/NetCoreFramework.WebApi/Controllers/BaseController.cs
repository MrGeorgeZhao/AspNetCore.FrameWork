using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreFramework.Domain.Interfaces;
using NetCoreFramework.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using NLog;
using Microsoft.AspNetCore.Authorization;
using NetCoreFramework.Domain.Models;
using System.Security.Claims;
using Newtonsoft.Json;

namespace NetCoreFramework.WebApi.Controllers
{

    [Authorize]
    public class BaseController : ControllerBase
    {
        public User CurrentUser
        {
            get
            {
                User user = new User();
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var claims = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Claims;
                    user.Id = long.Parse(claims.Where(p => p.Type == ClaimTypes.Sid).FirstOrDefault().Value);
                    user.Name = claims.Where(p => p.Type == ClaimTypes.Name).FirstOrDefault().Value;
                }

                return user;
            }
        }
    }
}
