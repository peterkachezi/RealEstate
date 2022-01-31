using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RealEstateManager.Data.DTOs.ApplicationUsersModule;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.CountyModule;
using RealEstateManager.Data.Services.SMSModule;
using RealEstateManager.EmailServiceModule;
using RealEstateManager.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PasswordOptions = RealEstateManager.Extensions.PasswordOptions;

namespace RealEstateManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        private readonly SignInManager<AppUser> signInManager;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IEmailService emailService;

        private readonly IMessagingService messagingService;

        private readonly ICountyService countyService;

        private readonly IConfiguration config;

        private IWebHostEnvironment env;


        public UserManagerController(IMessagingService messagingService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,

        RoleManager<IdentityRole> roleManager, IEmailService emailService, IConfiguration config, IWebHostEnvironment env,ICountyService countyService)
        {
            this.userManager = userManager;

            this.signInManager = signInManager;

            this.roleManager = roleManager;      

            this.emailService = emailService;

            this.countyService = countyService;

            this.config = config;

            this.env = env;

            this.messagingService = messagingService;

        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Roles = await roleManager.Roles.ToListAsync();

            ViewBag.Counties = await countyService.GetAll();

            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }


        public async Task<ActionResult> GetUsers()
        {
            var doctor = (await userManager.Users.ToListAsync());

            var doctors = new List<ApplicationUserDTO>();

            foreach (var item in doctor)
            {
                var data = new ApplicationUserDTO
                {
                    Id = item.Id,

                    Email = item.Email,

                    FirstName = item.FirstName,

                    LastName = item.LastName,

                    FullName = item. FirstName + " " + item. LastName,

                    PhoneNumber = item.PhoneNumber,

                    CreateDate = item.CreateDate,

                    isActive = item.isActive,

                };

                doctors.Add(data);
            }

            return Json(new { data = doctor });

        }
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterDTO registerDTO)
        {
            try
            {
                string password = PasswordStore.GenerateRandomPassword(new PasswordOptions
                {
                    RequiredLength = 8,

                    RequireNonLetterOrDigit = true,

                    RequireDigit = true,

                    RequireLowercase = true,

                    RequireUppercase = true,

                    RequireNonAlphanumeric = true,

                    RequiredUniqueChars = 1
                });

                registerDTO.Password = password;


                var user = new AppUser()
                {
                    UserName = registerDTO.Email,

                    Email = registerDTO.Email,

                    isActive = true,

                    PhoneNumber = registerDTO.PhoneNumber,

                    FirstName = registerDTO.FirstName,

                    LastName = registerDTO.LastName,

                    CreateDate = DateTime.Now,

                };

                var result = await userManager.CreateAsync(user, registerDTO.Password);

                var getloggedUser = await userManager.FindByEmailAsync(User.Identity.Name);

                registerDTO.CreatedBy = getloggedUser.Id;                              

                var sendEmail = emailService.SendAccountCreationEmailNotification(registerDTO);

                var sms = messagingService.usersAccount(registerDTO);

                var createRole = await userManager.AddToRoleAsync(user, registerDTO.RoleName);

                if (result.Succeeded)
                {
                    return Json(new { success = true, responseText = "Account has been created successfully" });
                }

                foreach (var error in result.Errors)
                {
                    return Json(new { success = false, responseText = "Unable to update record report details" });

                }

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        //public async Task<IActionResult> ActivateAccount(string Id, RegisterDTO registerDTO)
        //{
        //    try
        //    {
        //        var user = await userManager.FindByIdAsync(Id);

        //        user.isActive = true;

        //        var activate = await userManager.UpdateAsync(user);

        //        registerDTO.Email = user.Email;

        //        registerDTO.FullNames = user.FullName;

        //        registerDTO.Message = "Your account has been activated";

        //        var sendMail = emailService.SendActivationDeactivationNotification(registerDTO);

        //        return Json(new { success = true, responseText = "Account has been activated successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return null;
        //    }

        //}


        //public async Task<IActionResult> DeactivateAccount(string Id, RegisterDTO registerDTO)
        //{
        //    try
        //    {
        //        var user = await userManager.FindByIdAsync(Id);

        //        var getUserRole = (await userManager.GetRolesAsync(user)).FirstOrDefault();

        //        if (getUserRole == "Administrator")
        //        {
        //            return Json(new { success = false, responseText = "Unable to deactivate Administrator Account" });

        //        }
        //        user.isActive = false;

        //        registerDTO.Email = user.Email;

        //        registerDTO.FullNames = user.FullName;

        //        registerDTO.Message = "Your account has been deactivated";

        //        var sendMail = emailService.SendActivationDeactivationNotification(registerDTO);

        //        var activate = await userManager.UpdateAsync(user);

        //        return Json(new { success = true, responseText = "Account has been deactivate successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return null;
        //    }

        //}

        public async Task<ActionResult> GetUserById(string Id)
        {
            try
            {
                var data = await userManager.FindByIdAsync(Id);

                if (data != null)
                {
                    RegisterDTO file = new RegisterDTO()
                    {
                        Id = data.Id,

                        Email = data.Email,

                        PhoneNumber = data.PhoneNumber,

                        FullNames = data.FirstName +" " + data.LastName,

                    };

                    return Json(new { Data = file });
                }

                return Json(new { Data = false });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<IActionResult> UpdateUserDetails(string Id, RegisterDTO registerDTO)
        {
            try
            {
                var user = await userManager.FindByIdAsync(Id);

                var getUserRole = (await userManager.GetRolesAsync(user)).FirstOrDefault();

                user.FirstName = registerDTO.FirstName;

                user.LastName = registerDTO.LastName;

                var update = await userManager.UpdateAsync(user);

                return Json(new { success = true, responseText = "Account has been updated successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
    }
}
