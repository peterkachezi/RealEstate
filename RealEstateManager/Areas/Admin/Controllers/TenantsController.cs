using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateManager.Data.DTOs.TenantModule;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.ApartmentModule;
using RealEstateManager.Data.Services.CountyModule;
using RealEstateManager.Data.Services.HouseModule;
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
        private readonly ItenantService tenantService;

        private readonly ICountyService  countyService;

        private readonly IApartmentService   apartmentService;

        private readonly IHouseService houseService;

        private readonly UserManager<AppUser> userManager;

        private IWebHostEnvironment env;
        public TenantsController(IHouseService houseService,IApartmentService apartmentService,ICountyService countyService,IWebHostEnvironment env,UserManager<AppUser> userManager,ItenantService tenantService)
        {
            this.env = env;

            this.tenantService = tenantService;

            this.userManager = userManager;

            this.countyService = countyService;

            this.apartmentService = apartmentService;

            this.houseService = houseService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.County = await countyService.GetAll();

            ViewBag.Apartment = await apartmentService.GetAll();

            return View();
        }

        public async Task<IActionResult> GetTenants()
        {
            try
            {
                var tenants = (await tenantService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = tenants });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await tenantService.Delete(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record deleted successfully " });
                }
                else
                {
                    return Json(new { success = false, responseText = "Record has not been deleted ,it could be in use by other records" });
                }
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
                var isCarExist = (await tenantService.GetAll()).Where(x=>x.Email==tenantDTO.Email).Count();

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

                var result = tenantService.Create(tenantDTO);


                if (result != null)
                {
                    return Json(new { success = true, responseText = "Tenant has been successfully added" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Unable to add Tenant" });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ActionResult> GetHouses(Guid Id)
        {
            try
            {               

                var all = await houseService.GetAll();

                var streams = all.Where(x => x.ApartmentId == Id).ToList();

                return Json(streams.Select(x => new
                {
                    HouseId = x.Id,

                    HouseName = x.Name

                }).ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
    }
}
