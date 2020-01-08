using Doctorly.CodeTest.Data;
using Doctorly.CodeTest.Provider.Interfaces;
using Doctorly.CodeTest.Provider.Providers;
using Doctorly.CodeTest.Repository.DTOS;
using Doctorly.CodeTest.REST.Exceptions;
using Doctorly.CodeTest.REST.Swagger;
using Doctorly.CodeTestRepository;
using Doctorly.CodeTestRepository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Doctorly.CodeTest.REST
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Configuration["ServiceConfig:OS"] = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            Configuration["ServiceConfig:Environment"] = env.EnvironmentName;
            Configuration["ServiceConfig:ServiceVersion"] = Environment.GetEnvironmentVariable("BUILD_VERSION") ?? "1.0.0.0";
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddOptions();
            //here I'm creating the instances of the global objects used fot heath method and swagger
            services.Configure<ServiceConfigOptions>(Configuration.GetSection("ServiceConfig"));
            services.Configure<SwaggerInfo>(Configuration.GetSection("SwaggerInfo"));
            //I'm crearte here the configuration for the .net core native dependecy ingection
            services.AddScoped<IDataContext, UserContext>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IUserRepository, UserRepository>();

            var swaggerInfo = Configuration.GetSection("SwaggerInfo").Get<SwaggerInfo>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    swaggerInfo.Version,
                    new Info
                    {
                        Title = swaggerInfo.Description,
                        Version = swaggerInfo.Version
                    });
                c.DocumentFilter<SwaggerUIFilter>();
                c.OrderActionsBy(p => p.HttpMethod);
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration, IDataContext context)
        {

            var swaggerInfo = Configuration.GetSection("SwaggerInfo").Get<SwaggerInfo>();
            app.UseCors(builder =>
                builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseHsts();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{swaggerInfo.RootPath}/swagger/{swaggerInfo.Version}/swagger.json", swaggerInfo.Description);                
            });
            app.UseMiddleware<ExceptionHandler>();
            app.UseAuthentication();
            app.UseMvc();
        }
    }

}
