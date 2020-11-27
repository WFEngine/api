using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace WFEngine.Bootstrapper
{
    public static class FluentValidatorBootstrapper
    {
        public static IMvcBuilder AddFluentValidatorBootstrapper(this IMvcBuilder mvc,List<Type> validators)
        {
            mvc.AddFluentValidation(fv=> {
                foreach (var validator in validators)
                {
                    fv.RegisterValidatorsFromAssemblyContaining(validator);
                }
            });            
            return mvc;
        }
    }
}
