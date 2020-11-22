using Microsoft.Extensions.DependencyInjection;

namespace WFEngine.Bootstrapper
{
    public static class AutoMapperBootstrapper
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            return services;
        }
    }
}
