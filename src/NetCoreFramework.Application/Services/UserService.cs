using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreFramework.Application.Dto;
using NetCoreFramework.Application.Dto.Error;
using NetCoreFramework.Application.Dto.User;
using NetCoreFramework.Application.Interfaces;
using NetCoreFramework.Domain.Interfaces;
using NetCoreFramework.Domain.Models;
using NetCoreFramework.Infra.Util.Jwt;
using System.Linq;

namespace NetCoreFramework.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;


        public UserService(IMapper mapper,
                                  IUserRepository userRepository,
                                  IEncryptionService encryptionService)
        {

            _mapper = mapper;
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }

        public async Task<ResponseWrapper<LoginResponse>> Login(LoginRequest request, IOptions<JwtConfig> options)
        {
            var ret = new ResponseWrapper<LoginResponse>();

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.PassWord))
            {
                ret.Error = ErrorResponse.FromCode(ErrorCode.UserOrPassEmpty);
                return ret;
            }

            var user = await _userRepository.GetUserByName(request.Name);
            if (!user.Any())
            {
                ret.Error = ErrorResponse.FromCode(ErrorCode.UserNotExisit);
                return ret;
            }

            string thePass = _encryptionService.GetDigestedPassword(request.PassWord, user.FirstOrDefault().Salt);
            if (!thePass.Equals(user.FirstOrDefault().PassWord))
            {
                ret.Error = ErrorResponse.FromCode(ErrorCode.UserOrPassWrong);
                return ret;
            }



            var claims = new[]
                   {
                             new Claim(ClaimTypes.Name, request.Name),
                             new Claim(ClaimTypes.Sid,user.FirstOrDefault().MemberId.ToString())
                 };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(options.Value.ExpiresHour),
                signingCredentials: creds);


            ret.Result = new LoginResponse()
            {
                IsSuccess = true,

                Uid = user.FirstOrDefault().MemberId,

                UserName = user.FirstOrDefault().UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            return ret;
        }



    }
}
