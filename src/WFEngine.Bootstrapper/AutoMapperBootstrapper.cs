using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WFEngine.Bootstrapper
{
    public static class AutoMapperBootstrapper
    {
        public static IServiceCollection AddMapper(this IServiceCollection services,Type type)
        {
            services.AddAutoMapper(type);
            return services;
        }
    }
}
