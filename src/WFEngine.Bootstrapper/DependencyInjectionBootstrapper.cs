using Microsoft.Extensions.DependencyInjection;
using WFEngine.Core.Interfaces;
using WFEngine.Service;
using WFEngine.Service.Repositories;

namespace WFEngine.Bootstrapper
{
    public static class DependencyInjectionBootstrapper
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISolutionRepository, SolutionRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            return services;
        }
    }
}
