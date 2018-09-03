using System;
using AutoMapper;
using NetCoreFramework.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreFramework.WebApi.Configurations
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper();
            AutoMapperConfig.RegisterMappings();
        }
    }
}