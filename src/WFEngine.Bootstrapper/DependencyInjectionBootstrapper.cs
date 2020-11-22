using Microsoft.Extensions.DependencyInjection;

namespace WFEngine.Bootstrapper
{
    public static class DependencyInjectionBootstrapper
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            return services;
        }
    }
}
