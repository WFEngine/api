using Microsoft.Extensions.DependencyInjection;
using WFEngine.Core.Interfaces;
using WFEngine.Service;

namespace WFEngine.Bootstrapper
{
    public static class DependencyInjectionBootstrapper
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
