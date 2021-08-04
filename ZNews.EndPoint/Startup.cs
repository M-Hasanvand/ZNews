using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZNews.Application.InterFaces.Context;
using ZNews.Application.InterFaces.FacadPatterns;
using ZNews.Application.Services.Categories.FacadPattern;
using ZNews.Application.Services.Comments.FacadPattern;
using ZNews.Application.Services.HomePageSite.FacadPattern;
using ZNews.Application.Services.Menus.FacadPattern;
using ZNews.Application.Services.News.FacadPattern;
using ZNews.Application.Services.Tags.FacadPattern;
using ZNews.Application.Services.Users.FacadPattern;
using ZNews.Common.Roles;
using ZNews.Domain.Entities.Users;
using ZNews.Persistence.Context;

namespace ZNews.EndPoint
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
            services.AddMvc();
            services.AddAuthorization(options=>
            {
                options.AddPolicy(UserRole.Admin,policy=>policy.RequireRole(UserRole.Admin));
                options.AddPolicy(UserRole.Reporter, policy=>policy.RequireRole(UserRole.Reporter));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(option=>
            {
                option.LoginPath = new PathString("/Admin/Authentication/Sginin");
                option.AccessDeniedPath = new PathString("/Admin/Authentication/Sginin");
                option.ExpireTimeSpan = TimeSpan.FromDays(50);
            });
            ///---------------------------ConnecionString-Database{Db_Znews}----------------|
            string connecionstring = Configuration.GetConnectionString("ZnewsDb");
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(options => options.UseSqlServer(connecionstring));
            ///------------------DataBaseContext---------------------------|
            services.AddScoped<IDataBaseContext, DataBaseContext>();
            ///---------------------------FacadPattern----------------|
            services.AddScoped<IUserFacad, UserFacadForAdmin>();
            services.AddScoped<ITagFacad, TagFacad>();
            services.AddScoped<ICategoryFacad,CategoryFacad>();
            services.AddScoped<INewsFacadForAdmin, NewsFacadForAdmin>();
            services.AddScoped<INewsFacadForSite, NewsFacadForSite>();
            services.AddScoped<ICommentFacad, CommentFacad>();
            services.AddScoped<IMenusFacadForAdmin, MenusFacadForAdmin>();
            services.AddScoped<IMenuFacadForSite, MenuFacadForSite>();
            services.AddScoped<IHomePageFacad,HomePageFacad>();
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
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
