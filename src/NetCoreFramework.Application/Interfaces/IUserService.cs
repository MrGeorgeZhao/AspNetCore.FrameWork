using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreFramework.Application.Dto;
using NetCoreFramework.Application.Dto.User;
using NetCoreFramework.Infra.Util.Jwt;

namespace NetCoreFramework.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseWrapper<LoginResponse>> Login(LoginRequest request, IOptions<JwtConfig> options);
    }
}
