using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using WFEngine.Api.Dto.Request.Auth;
using WFEngine.Api.Dto.Request.Solution;
using WFEngine.Bootstrapper;

namespace WFEngine.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public Startup()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddMapper(typeof(Startup));
            mvcBuilder.AddFluentValidatorBootstrapper(new List<Type>
            {
                typeof(RegisterRequestDTO),
                typeof(LoginRequestDTO),
                typeof(InsertSolutionRequestDTO)
            });
            services.AddDependencyInjection();
            services.AddLocalizationMessage();
            services.AddSwagger();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseLocalizationMessage();
            app.UseSwaggerGen();
            app.UseCors(x => x
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
