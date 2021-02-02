using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCore.data;
using MvcCore.Helpers;
using MvcCore.Repositories;

namespace MvcCore
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            String cadenasql = this.Configuration.GetConnectionString("cadenasql");
            services.AddTransient<IRepositoryCoches, RepositoryCochesSQL>();
            services.AddDbContext<CochesContext>(options => options.UseSqlServer(cadenasql));

            //String cadenamyql = this.Configuration.GetConnectionString("cadenamysql");
            //services.AddTransient<IRepositoryCoches, RepositoryCochesMYSQL>();
            //services.AddDbContext<CochesContext>(options => options.UseMySql(cadenamyql, ServerVersion.AutoDetect(cadenamyql)));

            services.AddSingleton<PathProvider>();
            //services.AddTransient<IRepositoryCoches, RepositoryCochesXML>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
