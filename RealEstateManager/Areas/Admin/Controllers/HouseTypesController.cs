using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateManager.Data.DTOs.HouseTypeModule;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.HouseTypeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HouseTypesController : Controller
    {
        private readonly IHouseTypeService  houseTypeService;
        private readonly UserManager<AppUser> userManager;
        public HouseTypesController(UserManager<AppUser> userManager, IHouseTypeService houseTypeService)
        {
            this.houseTypeService = houseTypeService;

            this.userManager = userManager;

        }
        public IActionResult Index()
        {      
            return View();
        }
        public async Task<IActionResult> GetHouseTypes()
        {
            try
            {
                var landlords = (await houseTypeService.GetAll()).OrderBy(x => x.CreateDate).ToList();

                return Json(new { data = landlords });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<IActionResult> Create(HouseTypeDTO houseTypeDTO)
        {
            try
            {
                var getlead = (await houseTypeService.GetAll()).Where(x => x.Name == houseTypeDTO.Name).ToList();

                if (getlead.Count > 0)
                {
                    return Json(new { success = false, responseText = "Name already exist" });
                }

                else
                {
                    var user = await userManager.FindByEmailAsync(User.Identity.Name);

                    houseTypeDTO.CreatedBy = user.Id;

                    var results = await houseTypeService.Create(houseTypeDTO);

                    if (results != null)
                    {
                        return Json(new { success = true, responseText = "Record added successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "Failed to add record!" });
                    }
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
                var results = await houseTypeService.Delete(Id);

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
        public async Task<IActionResult> Update(HouseTypeDTO houseTypeDTO)
        {
            try
            {
                var results = await houseTypeService.Update(houseTypeDTO);

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
                var data = await houseTypeService.GetById(Id);

                if (data != null)
                {
                    HouseTypeDTO file = new HouseTypeDTO
                    {
                        Id = data.Id,                  

                        Name = data.Name,

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
