using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace District09.Servicefactory.Weekdagen.Domain.Configuration
{
    public static class ConfigureDomainExtensions
    {
        private static IServiceCollection ConfigureCommon(IServiceCollection services)
        {
            return services;
        }


        public static IServiceCollection ConfigureDevelopmentResources(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return ConfigureCommon(services);
        }

        public static IServiceCollection ConfigureResources(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return ConfigureCommon(services);
        }
    }
}