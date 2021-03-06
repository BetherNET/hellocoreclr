﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HelloCoreClrApp.WebApi
{
    public class Startup
    {
        private const string ApiVersion = "v1";

        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<Startup>();
        public static Container Container { private get; set; }

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Log.Information("Starting up in {0} mode.", env.EnvironmentName);

            //add SeriLog to ASP.NET Core
            loggerFactory.AddSerilog();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("Configuring services.");

            // Add framework services.
            services.AddMvc();
            services.AddMvcCore()
                .AddJsonFormatters(options =>
                    options.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddSwaggerGen(SetupSwagger);

            // Add SimpleInjector Controller Activator
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(Container));
        }

        private static void SetupSwagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Info
                {
                    Title = "Hello CoreCLR Service API",
                    Description = "Just a playground...",
                    TermsOfService = "None",
                    Version = ApiVersion
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app)
        {
            Log.Information("Configuring request pipeline.");

            // Serve the default file, if present.
            app.UseDefaultFiles();
            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi(c => 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiVersion));

            app.UseSimpleInjectorAspNetRequestScoping(Container);
        }
    }
}
