using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.ApartmentModule;
using RealEstateManager.Data.Services.CountyModule;
using RealEstateManager.Data.Services.HouseModule;
using RealEstateManager.Data.Services.HouseTypeModule;
using RealEstateManager.Data.Services.LandlordModule;
using RealEstateManager.Data.Services.SMSModule;
using RealEstateManager.Data.Services.TenantModule;
using RealEstateManager.EmailServiceModule;
using RealEstateManager.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManager
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddScoped<ILandlordService, LandlordService>();

            services.AddScoped<ItenantService, TenantService>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IMessagingService, MessagingService>();

            services.AddScoped<ICountyService, CountyService>();

            services.AddScoped<IApartmentService, ApartmentService>();

            services.AddScoped<IHouseTypeService, HouseTypeService>();

            services.AddScoped<IHouseService, HouseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
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
            CreateRoles(roleManager);

            CreateUsers(userManager);       

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "Admin",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "Agent",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (!roleManager.RoleExistsAsync("Admin").Result)

                {
                    var role = new IdentityRole();

                    role.Name = "Admin";

                    roleManager.CreateAsync(role);
                }

                if (!roleManager.RoleExistsAsync("Agent").Result)
                {
                    var role = new IdentityRole();

                    role.Name = "Agent";

                    roleManager.CreateAsync(role);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }


        private void CreateUsers(UserManager<AppUser> userManager)
        {
            try
            {
                var admin = userManager.FindByEmailAsync("admin@gmail.com");

                if (admin.Result == null)
                {
                    var user = new AppUser();

                    user.UserName = "admin@gmail.com";

                    user.Email = "admin@gmail.com";

                    user.PhoneNumber = "0704509484";

                    user.FirstName = "Alex";

                    user.LastName = "Jobs";

                    user.EmailConfirmed = true;

                    user.isActive = true;

                    user.CreateDate = DateTime.Now;

                    string userPWD = "Admin@2022";

                    var chkUser = userManager.CreateAsync(user, userPWD);

                    //Add default User to Role Admin    
                    if (chkUser.Result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();

                    }

                }

                var agent = userManager.FindByEmailAsync("agent@gmail.com");

                if (agent.Result == null)
                {
                    var user = new AppUser();

                    user.UserName = "agent@gmail.com";

                    user.Email = "agent@gmail.com";

                    user.PhoneNumber = "0704509484";

                    user.FirstName = "Peter";

                    user.LastName = "Kachezi";

                    user.EmailConfirmed = true;

                    user.isActive = true;

                    user.CreateDate = DateTime.Now;

                    user.Commissions = 15;

                    string userPWD = "Agent@2022";

                    var chkUser = userManager.CreateAsync(user, userPWD);

                    //Add default User to Role Admin    
                    if (chkUser.Result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Agent").Wait();

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
