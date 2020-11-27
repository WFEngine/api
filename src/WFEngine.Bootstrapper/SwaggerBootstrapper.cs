using environment.net.core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WFEngine.Bootstrapper
{
    public static class SwaggerBootstrapper
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            EnvironmentManager environmentManager = EnvironmentManager.Instance;
            if (environmentManager.IsDevelopment())
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WFEngine.Api", Version = "v1" });
                });
            return services;
        }

        public static IApplicationBuilder UseSwaggerGen(this IApplicationBuilder app)
        {
            EnvironmentManager environmentManager = EnvironmentManager.Instance;
            if (environmentManager.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WFEngine.Api v1"));
            }
            return app;
        }
    }
}
