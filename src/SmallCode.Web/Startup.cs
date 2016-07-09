using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmallCode.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SmallCode.Web.Services;
using SmallCode.Web.Services.Impl;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace SmallCode.Web
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SCContext>(option => option.UseNpgsql(Configuration.GetConnectionString("PostgreSql")));

            services.AddMvc();
            services.AddSession();
            services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            //csrf
            services.AddAntiforgery();
            services.AddCors();
            services.AddSignalR();
            //services.AddSignalR(options =>
            //{
            //    options.Hubs.EnableDetailedErrors = true;
            //});
            services.AddSingleton<ConnectionManager>();

            ///注入services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMaterialsService, MaterialsService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
            services.AddScoped<IAskService, AskService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEXArticleAuthorService, EXArticleAuthorService>();
            services.AddScoped<IEXArticleService, EXArticleService>();
            services.AddScoped<IEXArticleTempService, EXArticleTempService>();
            services.AddScoped<IMaterialsCategoryService, MaterialsCategoryService>();
            services.AddScoped<IProgrammingService, ProgrammingService>();
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseSession();
            app.UseSignalR();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = new PathString("/Account/Login"),
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,

            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            }
          );
            await SampleData.InitDB(app.ApplicationServices);
        }
    }
}
