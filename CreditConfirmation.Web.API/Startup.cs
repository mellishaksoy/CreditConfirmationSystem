using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CreditConfirmation.Web.API.Application.Services;
using CreditConfirmation.Web.API.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CreditConfirmation.Web.API
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
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var basePath = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder();

            switch (environment)
            {
                case "Development":
                    builder.SetBasePath(basePath)
                        .AddJsonFile($"appsettings.{environment}.json", optional: true);
                    break;
                case "Production":
                    builder.SetBasePath(basePath)
                         .AddJsonFile($"appsettings.{environment}.json", optional: true);
                    break;
                case "Test":
                    builder.SetBasePath(basePath)
                        .AddJsonFile($"appsettings.{environment}.json", optional: true);
                    break;
                default:
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true);
                    break;
            }


            var configuration = builder.Build();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            var appSettings = configuration.Get<AppSettings>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHttpClient("CreditConfirmationApi", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetSection("AppSettings:CreditConfirmationServiceApiUrl").Value);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddTransient<ICreditConfirmationService, CreditConfirmationService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
