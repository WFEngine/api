using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace WFEngine.Bootstrapper
{
    public static class LocalizationBootstrapper
    {
        public static IServiceCollection AddLocalizationMessage(this IServiceCollection services)
        {
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "";
            });
            return services;
        }

        public static IApplicationBuilder UseLocalizationMessage(this IApplicationBuilder app)
        {
            var supportedCultures = new[]
           {
                new CultureInfo("en"),
                new CultureInfo("tr")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            return app;            
        }
    }
}
