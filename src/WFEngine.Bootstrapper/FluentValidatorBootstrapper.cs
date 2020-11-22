using Microsoft.Extensions.DependencyInjection;

namespace WFEngine.Bootstrapper
{
    public static class FluentValidatorBootstrapper
    {
        public static IMvcBuilder AddFluentValidator(this IMvcBuilder mvc)
        {
            return mvc;
        }
    }
}
