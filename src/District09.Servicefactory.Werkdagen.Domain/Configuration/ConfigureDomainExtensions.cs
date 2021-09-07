using System;
using District09.Servicefactory.Werkdagen.Domain.Contracts;
using District09.Servicefactory.Werkdagen.Domain.Data;
using District09.Servicefactory.Werkdagen.Domain.Data.Providers;
using District09.Servicefactory.Werkdagen.Domain.Dto;
using District09.Servicefactory.Werkdagen.Domain.Mappers;
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
            services.AddSingleton<IWorkdayRepository, WorkdayRepository>();
            services.AddScoped<IMapper<DateTime, DayDto>, WorkDayMapper>();
            return services;
        }
    }
}