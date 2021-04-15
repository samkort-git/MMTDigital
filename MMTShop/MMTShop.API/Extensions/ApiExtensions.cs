using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace MMTShop.Extensions
{
    public static class ApiExtensions
    {
        private const string corsPolicyName = "default";
        private const string loggerName = "API";

        /// <summary>
        /// Add CORS. This defines a CORS policy called "default" (Note: this is for javascript client).
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <returns>Service Collection</returns>
        public static IServiceCollection AddCORS(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
         
            return services;
        }

        /// <summary>
        /// Add Default Mvc
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <returns>MVC Builder</returns>
        public static IMvcBuilder AddDefaultMvc(this IServiceCollection services)
        {
            var mvcBuilder = services.AddControllers()
                .AddDefaultMvcOptions()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            return mvcBuilder;
        }

        /// <summary>
        /// Add Default Mvc Options.
        /// </summary>
        /// <param name="mvcBuilder">Mvc Builder</param>
        /// <returns>Mvc Builder</returns>
        public static IMvcBuilder AddDefaultMvcOptions(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddMvcOptions(options =>
                {
                    options.Filters.Add(typeof(ValidatorActionFilter));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            return mvcBuilder;
        }

        /// <summary>
        /// Use CORS. This defines a CORS policy called "default" (Note: this is for javascript client).
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseCORS(this IApplicationBuilder app)
        {
            return app.UseCors(corsPolicyName);
        }

        /// <summary>
        /// Use Application Lifetime.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseApplicationLifetime(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetService<ILoggerFactory>().CreateLogger(loggerName);
            var appLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            var hostingEnvironment = app.ApplicationServices.GetService<IWebHostEnvironment>();
            var appName = hostingEnvironment.ApplicationName;

            // Register application events
            appLifetime.ApplicationStarted.Register(() => { logger.LogInformation($"{appName} started."); });
            appLifetime.ApplicationStopping.Register(() => { logger.LogWarning($"{appName} stopping."); });
            appLifetime.ApplicationStopped.Register(() => { logger.LogWarning($"{appName} stopped."); });

            return app;
        }

        /// <summary>
        /// Use Error Handling.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        /// <summary>
        /// Use Request Pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseRequestPipeline(this IApplicationBuilder app)
        {
            // Adds a middleware delagate defined in-line to the application's request pipeline.
            app.Use(async (context, next) =>
            {
                await next();
                // If there's no available file and the request doesn't contain an extension, we're probably trying to access a page.
                // Rewrite request to use app root
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api"))
                {
                    context.Request.Path = "/index.html";
                    context.Response.StatusCode = 200; // Make sure we update the status code, otherwise it returns 404
                    await next();
                }
            });
            return app;
        }
    }
}

