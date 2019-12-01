using DemoRefit.Handlers;
using DemoRefit.HttpClients;
using DemoRefit.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System;

namespace DemoRefit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddRefitClient<ICountryRepository>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetSection("Apis:CountryApi:Url").Value));

            services.AddHttpClient<ICountryRepositoryClient, CountryRepositoryClient>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetSection("Apis:CountryApi:Url").Value))
                    .AddHttpMessageHandler<MyDelegatingHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
