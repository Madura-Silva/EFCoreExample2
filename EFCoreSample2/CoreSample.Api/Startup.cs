using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CoreSample.Api.Config;
using CoreSample.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;


namespace CoreSample.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //configurationReader.SetConfigValues(Configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            services.AddOptions();
            

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                //API versioning : https://stackoverflow.com/questions/58834430/c-sharp-net-core-swagger-trying-to-use-multiple-api-versions-but-all-end-point

                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "My Test CustomerAPI",
                    Description = "A simple example ASP.NET Core Web API"
                });
                

                c.SwaggerDoc("v2.0", new OpenApiInfo
                {
                    Version = "v2.0",
                    Title = "My Test CustomerAPI 2",
                    Description = "A simple example ASP.NET Core Web API 2"
                    
                });
                c.OperationFilter<RemoveVersionParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                c.EnableAnnotations();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        //Autofac Register
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DependancyRegister());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler(new ExceptionHandlerOptions
            //    {
            //        ExceptionHandlingPath = "/error"
            //    });
            //}

            //app.UseExceptionHandler(new ExceptionHandlerOptions
            //{
            //    ExceptionHandlingPath = "/handleError"
            //});

            app.UseHttpsRedirection();
            app.UseSwagger();


            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "My API V1");
                //c.OAuthClientId("swagger");
                //c.OAuthAppName("Swagger UI");

                c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "My API V2");
            });
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
