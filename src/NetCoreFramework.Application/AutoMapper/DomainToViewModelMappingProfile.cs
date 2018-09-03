using AutoMapper;
using NetCoreFramework.Application.Dto;
using NetCoreFramework.Application.Dto.User;
using NetCoreFramework.Domain.Models;
using NetCoreFramework.Infra.Util.Extensions;

namespace NetCoreFramework.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //CreateMap<Teacher, TeacherDto>().ForMember(p => p.Status, opt => { opt.MapFrom(x => x.Status.ToTeacherStatus()); })
            //                                                     .ForMember(p => p.Position, opt => { opt.MapFrom(x => x.Position.ToTeacherPosition()); })
            //                                                      .ForMember(p => p.Title, opt => { opt.MapFrom(x => x.Title); });

        }
    }
}
