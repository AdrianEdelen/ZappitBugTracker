using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZappitBugTracker.Data;
using ZappitBugTracker.Helpers;
using ZappitBugTracker.Models;
using ZappitBugTracker.services;

namespace ZappitBugTracker
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    DataHelper.GetConnectionString(Configuration)));
            //deprecated
            //Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<BTUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddScoped<IBTRolesService, BTRolesService>();
            services.AddScoped<IBTProjectService, BTProjectService>();
            services.AddScoped<IBTHistoryService, BTHistoryService>();
            services.AddScoped<IBTAccessService, BTAccessService>();
            services.AddScoped<IBTImageService, BTImageService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailSender, EmailService>();
            services.AddScoped<IBTAttachmentService, BTAttachmentService>();
            services.AddScoped<IBTModalService, BTModalService>();
            services.AddScoped<IBTChartAndDisplay, BTChartAndDisplay>();

            services.AddControllersWithViews();
            services.AddRazorPages();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                    //change the page that is loaded when first coming to the site
                    pattern: "{controller=Landing}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
