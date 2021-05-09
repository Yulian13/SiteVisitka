using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SiteVisitka.loggers;
using SiteVisitka.Middlewares;
using SiteVisitka.Models.SQL_models.Works;
using SiteVisitka.Serviñes;

namespace SiteVisitka
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("WorkConnection");
            services.AddDbContext<WorksContext>(option =>
                option.UseSqlServer(connection));

            services.AddControllersWithViews()
                .AddNewtonsoftJson(opttion =>
                    opttion.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddSession(option =>
            {
                option.Cookie.Name = "InputStatus";
                option.Cookie.IsEssential = true;
            });

            services.AddScoped<ManagerLoginAdmin>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            if (false)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();

                var logger = loggerFactory.AddLoggerException(configuration).CreateLogger("FileLogger");
                app.UseMiddleware<CheckExceptionMiddleware>(Options.Create(logger));
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMiddleware<LoginAdminMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Main}/{id?}");
            });
        }
    }
}
