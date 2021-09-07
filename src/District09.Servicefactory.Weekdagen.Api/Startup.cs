using District09.Servicefactory.Weekdagen.Domain.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace District09.Servicefactory.Weekdagen.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void StartupConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }


        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.ConfigureDevelopmentResources(Configuration);
            StartupConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.ConfigureResources(Configuration);
            StartupConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}