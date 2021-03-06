﻿using CreditConfirmation.API.Application.Services;
using CreditConfirmation.API.Domain;
using CreditConfirmation.API.Infrastructure.Repositories;
using CreditConfirmation.API.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace CreditConfirmation.API
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

            services.Configure<DbSettings>(options =>
            {
                options.ConnectionString
                    = Configuration.GetSection("AppSettings:MongoConnection:ConnectionString").Value;
            });
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHttpClient("CreditScore.API", c =>
            {
                c.BaseAddress = new Uri(Configuration.GetSection("AppSettings:CreditScoreServiceUrl").Value);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddTransient<ICreditConfirmationService, CreditConfirmationService>();
            services.AddTransient<ICreditApplicationRepository, CreditApplicationRepository>();
            services.AddTransient<ICreditLimitFactory, CreditLimitFactory>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            app.UseStaticFiles();

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
