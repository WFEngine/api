using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WFEngine.Bootstrapper
{
    public static class SwaggerBootstrapper
    {
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
