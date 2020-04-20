using Ratelimited.GameSession.Database;
using DotNetCore.AspNetCore;
using DotNetCore.EntityFrameworkCore;
using DotNetCore.IoC;
using DotNetCore.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Ratelimited.GameSession.Application;
using VueCliMiddleware;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Ratelimited.GameSession.Web
{
    public static class Extensions
    {
        public static void AddContext(this IServiceCollection services)
        {
            var connectionString = services.GetConnectionString(nameof(Context));
            services.AddContextMigrate<Context>(options => options.UseSqlServer(connectionString));
        }

        public static void AddSecurity(this IServiceCollection services)
        {
            services.AddHash(10000, 128);
            services.AddJsonWebToken(Guid.NewGuid().ToString(), TimeSpan.FromHours(12));
            services.AddAuthenticationJwtBearer();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddFileExtensionContentTypeProvider();
            services.AddClassesInterfaces(typeof(IUserService).Assembly);
            services.AddClassesInterfaces(typeof(IUnitOfWork).Assembly);
        }

        public static void AddSpa(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
        }

        public static void UseSpa(this IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.UseSpa(spa =>
            {
                if (environment.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp";
                else
                    spa.Options.SourcePath = "dist";

                if (environment.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }

            });
        }
    }
}
