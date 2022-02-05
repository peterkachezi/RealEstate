using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateManager.Data.DTOs.ApartmentModule;
using RealEstateManager.Data.DTOs.HouseModule;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.ApartmentModule;
using RealEstateManager.Data.Services.CountyModule;
using RealEstateManager.Data.Services.HouseModule;
using RealEstateManager.Data.Services.HouseTypeModule;
using RealEstateManager.Data.Services.LandlordModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApartmentsController : Controller
    {
        private readonly IApartmentService  apartmentService;    
        
        private readonly ILandlordService landlordService;   
        
        private readonly ICountyService  countyService;          

        private readonly UserManager<AppUser> userManager;

        private readonly IHouseTypeService houseTypeService;

        private readonly IHouseService houseService;

        public ApartmentsController(IHouseService houseService,IHouseTypeService houseTypeService,ICountyService countyService,ILandlordService landlordService,UserManager<AppUser> userManager, IApartmentService apartmentService)
        {     
            this.userManager = userManager;

            this.apartmentService = apartmentService;

            this.landlordService = landlordService;

            this.countyService = countyService;

            this.houseTypeService = houseTypeService;

            this.houseService = houseService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.County = await apartmentService.GetAll();

            ViewBag.Landlord = await landlordService.GetAll();

            ViewBag.Counties = await countyService.GetAll();

            ViewBag.HouseTypes = await houseTypeService.GetAll();

            return View();
        }

        public async Task<IActionResult> GetApartment()
        {
            try
            {
                var landlords = (await apartmentService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> CreateHouse(HouseDTO houseDTO)
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
        public async Task<IActionResult> Create(ApartmentDTO apartmentDTO)
        {
            try
            {
                if (apartmentDTO.LandlordId == null || apartmentDTO.LandlordId == Guid.Empty)
                {
                    return Json(new { success = false, responseText = "Please select a landlord" });
                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                    apartmentDTO.CreatedBy = user.Id;

                    var results = await apartmentService.Create(apartmentDTO);

                    if (results != null)
                    {
                        return Json(new { success = true, responseText = "Apartment added successfully" });
                    }

                    else
                    {
                        return Json(new { success = false, responseText = "Failed to add Apartment!" });
                    }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> Update(ApartmentDTO apartmentDTO)
        {
            try
            {
                var results = await apartmentService.Update(apartmentDTO);

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
                var data = await apartmentService.GetById(Id);

                if (data != null)
                {
                    ApartmentDTO file = new ApartmentDTO
                    {
                        Id = data.Id,

                        LandlordId = data.LandlordId,

                        CountyId = data.CountyId,

                        Name = data.Name,

                        Estate = data.Estate,

                        PhysicalAddress = data.PhysicalAddress,

                        Town = data.Town,

                        CreateDate = data.CreateDate,

                        CreatedBy = data.CreatedBy,

                        CreatedByName = data.CreatedByName,

                        LandlordName = data.LandlordName,
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
