using CorrelationId;
using CorrelationId.DependencyInjection;
using Digipolis.Serilog.Elk.Configuration;
using District09.Servicefactory.Werkdagen.Api.Configuration;
using District09.Servicefactory.Werkdagen.Domain.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            var commonOptions = Configuration.Get<CommonOptions>();
            services.AddSingleton<IStartupFilter>(new PathBaseStartupFilter(commonOptions.AppNamespacePrefix));
            services.AddHttpContextAccessor();
            services.AddAllElasticApm();
            services.AddDefaultCorrelationId(opts =>
            {
                opts.RequestHeader = "CorrelationId";
                opts.ResponseHeader = "CorrelationId";
            });
            services.ConfigureResources(Configuration);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorrelationId();
            app.UseD09RequestLogging();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .WithMethods("GET")
                    .AllowAnyHeader()
                    .Build();
            });

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}