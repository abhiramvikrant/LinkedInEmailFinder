using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedEmailFinder.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LinkedEmailFinder.DataAccess.Repository;
using LinkedInEmailFinder.Models.UserFields;
using AutoMapper;


namespace LinkedEmailFinder.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(c=> c.AddProfile<AutoMapping>(),typeof(Startup));
            services.AddDbContext<LinkedInEmailFinder_DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LinkedInCon")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<LinkedInEmailFinder_DBContext>().AddDefaultTokenProviders();
            services.AddScoped(typeof(LinkedInEmailFinder_DBContext), typeof(LinkedInEmailFinder_DBContext));
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
