using District09.Servicefactory.Werkdagen.Domain.Contracts;
using District09.Servicefactory.Werkdagen.Domain.Data;
using District09.Servicefactory.Werkdagen.Domain.Data.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace District09.Servicefactory.Werkdagen.Domain.Configuration
{
    public static class ConfigureDomainExtensions
    {
        public static IServiceCollection ConfigureResources(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<ExcellConfigOptions>(configuration.GetSection(ExcellConfigOptions.Prefix));
            services.AddSingleton<IFreedayDataProvider, LocalFileFreedayDataProvider>();
            services.AddSingleton<IWerkdagRepository, WerkdagRepository>();
            return services;
        }
    }
}