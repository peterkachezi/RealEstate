using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateManager.Data.DTOs.TenantModule;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.CountyModule;
using RealEstateManager.Data.Services.TenantModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TenantsController : Controller
    {
        private readonly ItenantService itenantService;

        private readonly ICountyService  countyService;

        private readonly UserManager<AppUser> userManager;

        private IWebHostEnvironment env;
        public TenantsController(ICountyService countyService,IWebHostEnvironment env,UserManager<AppUser> userManager,ItenantService itenantService)
        {
            this.env = env;

            this.itenantService = itenantService;

            this.userManager = userManager;

            this.countyService = countyService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.County = await countyService.GetAll();

            return View();
        }

        public async Task<IActionResult> GetTenants()
        {
            try
            {
                var tenants = (await itenantService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = tenants });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }


   
        public async Task<IActionResult> Create(TenantDTO tenantDTO, IFormFile[] AttachmentName)
        {
            try
            {
                var isCarExist = (await itenantService.GetAll()).Where(x=>x.Email==tenantDTO.Email).Count();

                if (isCarExist > 0)
                {
                    return Json(new { success = false, responseText = "This tenants already exist in the system" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                tenantDTO.CreatedBy = user.Id;

                if (AttachmentName == null || AttachmentName.Length == 0)
                {

                    return Json(new { success = false, responseText = "Please upload photos / images / documents" });

                }
                else
                {
                    tenantDTO.AttachmentName = new List<string>();

                    foreach (IFormFile photo in AttachmentName)
                    {

                        //Getting FileName
                        var fileName = Path.GetFileName(photo.FileName);

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);

                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(myUniqueFileName, fileExtension);

                        var path = Path.Combine(this.env.WebRootPath, "tenant_uploads", newFileName);

                        var stream = new FileStream(path, FileMode.Create);

                        photo.CopyTo(stream);

                        tenantDTO.AttachmentName.Add(newFileName);
                    }
                }

                var result = itenantService.Create(tenantDTO);


                if (result != null)
                {
                    return Json(new { success = true, responseText = "The vehicle has been successfully registered" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Unable to registered the vehicle" });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

    }
}
