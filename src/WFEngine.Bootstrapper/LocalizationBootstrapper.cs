using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WFEngine.Bootstrapper
{
    public static class LocalizationBootstrapper
    {
        public static IServiceCollection AddLocalization(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
