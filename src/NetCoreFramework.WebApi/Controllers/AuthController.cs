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

namespace NetCoreFramework.WebApi.Controllers
{

    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("health")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
