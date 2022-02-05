using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateManager.Data.DTOs.ApplicationUsersModule;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.ApartmentModule;
using RealEstateManager.Data.Services.HouseModule;
using RealEstateManager.Data.Services.LandlordModule;
using RealEstateManager.Data.Services.TenantModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public readonly ItenantService itenantService;

        public readonly IApartmentService  apartmentService;

        public readonly ILandlordService  landlordService;

        public readonly IHouseService   houseService;

        private readonly UserManager<AppUser> userManager;
        public DashboardController(UserManager<AppUser> userManager,IHouseService houseService,ILandlordService landlordService,IApartmentService apartmentService,ItenantService itenantService)
        {
            this.itenantService = itenantService;

            this.apartmentService = apartmentService;

            this.landlordService = landlordService;

            this.houseService = houseService;

            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Tenants = (await itenantService.GetAll()).Count();

            ViewBag.Apartments = (await apartmentService.GetAll()).Count();

            ViewBag.Landlord = (await landlordService.GetAll()).Count();

            ViewBag.OccupiedHouses = (await houseService.GetAll()).Where(x => x.Availability == 1).Count();

            ViewBag.VacantHouses = (await houseService.GetAll()).Where(x=>x.Availability==0).Count();

            ViewBag.Agents = await userManager.Users.ToListAsync();

            return View();
        }



    }
}
