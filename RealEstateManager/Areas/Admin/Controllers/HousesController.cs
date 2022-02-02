using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateManager.Data.DTOs.HouseModule;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.ApartmentModule;
using RealEstateManager.Data.Services.HouseModule;
using RealEstateManager.Data.Services.HouseTypeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HousesController : Controller
    {
        private readonly IHouseService houseService;

        private readonly IApartmentService  apartmentService;

        private readonly IHouseTypeService houseTypeService;

        private readonly UserManager<AppUser> userManager;
        public HousesController(IHouseTypeService houseTypeService,IApartmentService apartmentService,UserManager<AppUser> userManager, IHouseService houseService)
        {
            this.houseService = houseService;

            this.userManager = userManager;

            this.apartmentService = apartmentService;

            this.houseTypeService = houseTypeService;

        }
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.Apartment = await apartmentService.GetAll();

            ViewBag.HouseTypes = await houseTypeService.GetAll();

            return View();
        }
        public async Task<IActionResult> GetHouses()
        {
            try
            {
                var landlords = (await houseService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<IActionResult> Create(HouseDTO houseDTO)
        {
            try
            {              
                    var user = await userManager.FindByEmailAsync(User.Identity.Name);

                    houseDTO.CreatedBy = user.Id;

                    var results = await houseService.Create(houseDTO);

                    if (results != null)
                    {
                        return Json(new { success = true, responseText = "Record added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Failed to add record!" });
                    }
               
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
                var results = await houseService.Delete(Id);

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
        public async Task<IActionResult> Update(HouseDTO houseDTO)
        {
            try
            {
                var results = await houseService.Update(houseDTO);

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
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                var data = await houseService.GetById(Id);

                if (data != null)
                {
                    HouseDTO file = new HouseDTO
                    {
                        Id = data.Id,

                        ApartmentId = data.ApartmentId,

                        ApartmentName = data.ApartmentName,

                        HouseTypeId = data.HouseTypeId,

                        Name = data.Name,

                        Availability = data.Availability,

                        Condition = data.Condition,

                        RentAmount = data.RentAmount,

                        CreateDate = data.CreateDate,

                        CreatedBy = data.CreatedBy,

                        CreatedByName = data.CreatedByName,
                    };

                    return Json(new { data = file });
                }
                return Json(new { data = false });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
