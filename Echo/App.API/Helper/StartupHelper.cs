using App.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using System;

namespace App.API.Helper
{
    public static class StartupHelper
    {
        public static void RegisterDbContext(IServiceCollection services, IConfiguration configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            string connectionString = GetConnectionString(configuration);

            //nLog connection string
            GlobalDiagnosticsContext.Set("connectionString", connectionString);

            services.AddDbContext<AppDBContext>(o =>
            {
                o.UseSqlServer(connectionString);
            }, serviceLifetime);

            if (serviceLifetime == ServiceLifetime.Scoped)
                services.AddScoped<DbContext, AppDBContext>();
            else if (serviceLifetime == ServiceLifetime.Singleton)
                services.AddSingleton<DbContext, AppDBContext>();
            else if (serviceLifetime == ServiceLifetime.Transient)
                services.AddTransient<DbContext, AppDBContext>();

        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            var hostname = Environment.GetEnvironmentVariable("SQLSERVER_HOST") ?? configuration["SQLSERVER:HOST"];
            var username = Environment.GetEnvironmentVariable("SQLSERVER_USERNAME") ?? configuration["SQLSERVER:USERNAME"];
            var password = Environment.GetEnvironmentVariable("SQLSERVER_PASSWORD") ?? configuration["SQLSERVER:PASSWORD"];
            var database = Environment.GetEnvironmentVariable("SQLSERVER_DATABASE") ?? configuration["SQLSERVER:DATABASE"];

            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //Configuration.GetConnectionString("DefaultConnection");
            connectionString = connectionString.Replace("{hostname}", hostname);
            connectionString = connectionString.Replace("{database}", database);
            connectionString = connectionString.Replace("{username}", username);
            connectionString = connectionString.Replace("{password}", password);
            return connectionString;
        }

        public static void ExceptionHandlerHelper(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("Global exception logger");
                        logger.LogError(500,
                            exceptionHandlerFeature.Error,
                            exceptionHandlerFeature.Error.Message);
                    }

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                });
            });
        }

    }
}
