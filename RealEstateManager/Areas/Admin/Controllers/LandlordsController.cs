using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateManager.Data.DTOs.LandlordModule;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.CountyModule;
using RealEstateManager.Data.Services.LandlordModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LandlordsController : Controller
    {
        private readonly ILandlordService landlordService;

        private readonly ICountyService countyService;

        private readonly UserManager<AppUser> userManager;
        public LandlordsController(ICountyService countyService, UserManager<AppUser> userManager, ILandlordService landlordService)
        {
            this.landlordService = landlordService;

            this.userManager = userManager;

            this.countyService = countyService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.County = await countyService.GetAll();

            return View();
        }

        public async Task<IActionResult> GetLandlords()
        {
            try
            {
                var landlords = (await landlordService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }


        public async Task<IActionResult> Create(LandlordDTO landlordDTO)
        {
            try
            {
                var getlead = (await landlordService.GetAll()).Where(x => x.Email == landlordDTO.Email && x.IdNumber == landlordDTO.IdNumber).ToList();

                if (getlead.Count > 0)
                {
                    return Json(new { success = false, responseText = "Email or Id Number already exist" });
                }

                else
                {
                    var user = await userManager.FindByEmailAsync(User.Identity.Name);

                    landlordDTO.CreatedBy = user.Id;

                    var results = await landlordService.Create(landlordDTO);

                    if (results != null)
                    {
                        return Json(new { success = true, responseText = "Landlord added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Failed to add landlord!" });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> Update(LandlordDTO landlordDTO)
        {
            try
            {
                var results = await landlordService.Update(landlordDTO);

                if (results != null)
                {
                    return Json(new { success = true, responseText = "Record updated successfully" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to update record!" });
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
