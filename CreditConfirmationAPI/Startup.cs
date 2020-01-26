using CreditConfirmation.API.Application.Services;
using CreditConfirmation.API.Domain;
using CreditConfirmation.API.Infrastructure.Repositories;
using CreditConfirmation.API.Settings;
using DataAccessHandler.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
            
            services.AddTransient<ICreditConfirmationService, CreditConfirmationService>();
            services.AddTransient<IUserRepository, UserRepository>();
            
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
            app.UseMvc();
        }
    }
}
