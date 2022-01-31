using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RealEstateManager.Data.DTOs.ApplicationUsersModule;
using RealEstateManager.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RealEstateManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<AppUser> userManager;

        private readonly IConfiguration _config;

        private IWebHostEnvironment _env;
        public AccountController(SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager, IConfiguration config, IWebHostEnvironment env)
        {

            this.signInManager = signInManager;

            this.roleManager = roleManager;

            this.userManager = userManager;

            _config = config;

            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(loginDTO.Email);

                    if (user == null)
                    {
                        TempData["Error"] = "Invalid user account / Account does not exist";

                        return RedirectToAction("Login", "Account");
                    }

                    if (user.isActive == false)
                    {
                        TempData["Error"] = "Your account has been disabled,kindly contact system administrator";

                        return RedirectToAction("Login", "Account");
                    }

                    var result = await signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, loginDTO.RemeberMe, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        var getUserRole = (await userManager.GetRolesAsync(user)).FirstOrDefault();

                        if (getUserRole == null)
                        {
                            TempData["Error"] = "The user has not been mapped to roles";

                            return RedirectToAction("Login", "Account");
                        }

                        if (getUserRole == "Admin")
                        {
                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                        }


                        if (getUserRole == "Agent")
                        {
                            return RedirectToAction("Index", "Dashboard", new { area = "Agent" });
                        }


                        else
                        {
                            return RedirectToAction("Login", "Account");
                        }

                    }

                    if (result.IsLockedOut)
                    {
                        TempData["Error"] = "This account has been locked out,please try again later";

                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        TempData["Error"] = "Wrong user name or password";

                        return RedirectToAction("Login", "Account");

                    }

                }

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {

            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(resetPasswordDTO.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.Password);

                    if (result.Succeeded)
                    {
                        await signInManager.RefreshSignInAsync(user);

                        return View("Login", "Account");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(resetPasswordDTO);

                }
                return View("ResetPasswordConfirmation");
            }
            return View(resetPasswordDTO);
        }




        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var user = await userManager.FindByEmailAsync(forgotPasswordDTO.Email);

                    if (user != null)
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);

                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = forgotPasswordDTO.Email, token }, Request.Scheme);

                        forgotPasswordDTO.ResetLink = passwordResetLink;

                        forgotPasswordDTO.FullName = user.FirstName + " " + user.LastName;

                        var sendEmail = SendEmailNotification(forgotPasswordDTO);

                        return Json(new { success = true, responseText = "If you have an account with us , we have sent a password resend link to " + forgotPasswordDTO.Email + "" });

                        //TempData["Info"] = "If you have an account with us , we have sent a password resend link to " + forgotPasswordDTO.Email + "";

                        //return RedirectToAction("Login", "Account");
                    }

                    return Json(new { success = true, responseText = "If you have an account with us , we have sent a password resend link to " + forgotPasswordDTO.Email + "" });


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return null;
                }
            }

            return View(forgotPasswordDTO);
        }

        public ActionResult SendEmailNotification(ForgotPasswordDTO forgotPasswordDTO)
        {
            //var claimDetails = _claimService.GetClaimById(claimDTO.Id);

            var SMTPEmailToNetwork = _config.GetValue<string>("MailSettings:SMTPEmailToNetwork");

            var SMTPMailServer = _config.GetValue<string>("MailSettings:SMTPMailServer");

            var SMTPPort = _config.GetValue<string>("MailSettings:SMTPPort");

            var SMTPUserName = _config.GetValue<string>("MailSettings:SMTPUserName");

            var Password = _config.GetValue<string>("MailSettings:Password");

            var SMTPUseSSL = _config.GetValue<string>("MailSettings:SMTPUseSSL");

            try
            {
                MailAddressCollection mailAddressesTo = new MailAddressCollection();

                mailAddressesTo.Add(new MailAddress(forgotPasswordDTO.Email));

                MailAddress mailAddressFrom = new MailAddress(SMTPUserName);

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = mailAddressFrom;

                foreach (var to in mailAddressesTo)
                    mailMessage.To.Add(to);


                mailMessage.Subject = "Healthier Kenya password reset instructions:-";

                var templatePath = _env.WebRootPath
                           + Path.DirectorySeparatorChar.ToString()
                           + "Templates"
                           + Path.DirectorySeparatorChar.ToString()
                           + "EmailTemplate"
                           + Path.DirectorySeparatorChar.ToString()
                           + "PasswordResetLink.html";

                var builder = new BodyBuilder();

                using (StreamReader SourceReader = System.IO.File.OpenText(templatePath))
                {

                    builder.HtmlBody = SourceReader.ReadToEnd();

                }

                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                mailMessage.Body = string.Format(builder.HtmlBody,

                     forgotPasswordDTO.FullName,

                     forgotPasswordDTO.ResetLink


                    );

                mailMessage.IsBodyHtml = true;

                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = SMTPMailServer;
                    client.Port = int.Parse(SMTPPort);
                    if (SMTPUseSSL != string.Empty)
                    {
                        client.EnableSsl = bool.Parse(SMTPUseSSL);
                    }

                    client.UseDefaultCredentials = false;
                    bool bNetwork = bool.Parse(SMTPEmailToNetwork);
                    if (bNetwork)
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    }
                    else
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    }

                    client.Credentials = new NetworkCredential(SMTPUserName, Password);
                    client.ServicePoint.MaxIdleTime = 2;
                    client.ServicePoint.ConnectionLimit = 1;
                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();
        }
    }
}
