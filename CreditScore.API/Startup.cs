using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CreditScore.API.Application.Services;
using CreditScore.API.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace CreditScore.API
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

            #region Swagger Configuration 

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(Configuration["AppSettings:SwaggerConfigurationInfo:ApiVersion"],
                    new OpenApiInfo
                    {
                        Title = Configuration["AppSettings:SwaggerConfigurationInfo:ApiName"],
                        Version = Configuration["AppSettings:SwaggerConfigurationInfo:ApiVersion"]
                    });

                options.CustomSchemaIds(x => x.FullName);
            });
            #endregion

            services.AddTransient<ICreditScoreService, CreditScoreService>();
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
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{Configuration["AppSettings:SwaggerConfigurationInfo:ApiVersion"]}/swagger.json", Configuration["AppSettings:SwaggerConfigurationInfo:ApiName"]);
            });

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=swagger}/{action=Index}");
            });
        }
    }
}
