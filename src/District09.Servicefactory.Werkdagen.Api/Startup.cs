using CorrelationId;
using CorrelationId.DependencyInjection;
using Digipolis.Microservices.Routing;
using Digipolis.Serilog.Elk.Configuration;
using District09.Servicefactory.Werkdagen.Api.Configuration;
using District09.Servicefactory.Werkdagen.Domain.Configuration;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace District09.Servicefactory.Werkdagen.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddDefaultCorrelationId(opts =>
            {
                opts.RequestHeader = "CorrelationId";
                opts.ResponseHeader = "CorrelationId";
            });
            services.ConfigureResources(Configuration);
            var commonOptions = Configuration.Get<CommonOptions>();
            services.AddControllers(options =>
                    options.UseGlobalRoutePrefix(new RouteAttribute(commonOptions.AppNamespacePrefix)))
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorrelationId();
            app.UseDigipolisRequestLogging();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseAllElasticApm();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}