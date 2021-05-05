using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiteVisitka.Middlewares;
using SiteVisitka.Models.SQL_models.Works;
using System;

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseAuthorization();

            app.UseMiddleware<LoginAdminMiddleware>();

            //var options = new RewriteOptions().AddRedirect("Admin[/]", "Admin/Index");
            //app.UseRewriter(options);

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
