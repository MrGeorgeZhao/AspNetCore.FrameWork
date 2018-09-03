using AutoMapper;
using NetCoreFramework.Application.Dto.User;
using NetCoreFramework.Domain.Models;

namespace NetCoreFramework.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LoginRequest, User>();
        }
    }
}
