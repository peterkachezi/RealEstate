using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateManager.Data.DTOs.LandlordModule;
using RealEstateManager.Data.Models;
using RealEstateManager.Data.Services.CountyModule;
using RealEstateManager.Data.Services.LandlordModule;
using RealEstateManager.Data.Services.SMSModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LandlordsController : Controller
    {
        private readonly ILandlordService landlordService;

        private readonly IMessagingService messagingService;

        private readonly ICountyService countyService;

        private readonly UserManager<AppUser> userManager;

        private IWebHostEnvironment env;
        public LandlordsController(IWebHostEnvironment env,IMessagingService messagingService,ICountyService countyService, UserManager<AppUser> userManager, ILandlordService landlordService)
        {
            this.landlordService = landlordService;

            this.userManager = userManager;

            this.countyService = countyService;

            this.messagingService = messagingService;

            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.County = await countyService.GetAll();

            return View();
        }

        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await landlordService.Delete(Id);

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


        public async Task<IActionResult> Create(LandlordDTO  landlordDTO, IFormFile[] AttachmentName)
        {
            try
            {
                var isCarExist = (await landlordService.GetAll()).Where(x => x.IdNumber == landlordDTO.IdNumber).Count();

                if (isCarExist > 0)
                {
                    return Json(new { success = false, responseText = "The Email or Id Number  already exist in the system" });

                }
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                landlordDTO.CreatedBy = user.Id;                          

                var result = landlordService.Create(landlordDTO);

                if (result != null)
                {

                   var sendsms = await messagingService.LandlordInfo(landlordDTO);

                    return Json(new { success = true, responseText = "Landlord added successfully" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Unable to add Landlord" });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IActionResult> Create1(LandlordDTO landlordDTO, IFormFile[] AttachmentName)
        {
            try
            {
                var isCarExist = (await landlordService.GetAll()).Where(x => x.IdNumber == landlordDTO.IdNumber).Count();

                if (isCarExist > 0)
                {
                    return Json(new { success = false, responseText = "The Email or Id Number  already exist in the system" });

                }

                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                landlordDTO.CreatedBy = user.Id;

                if (AttachmentName == null || AttachmentName.Length == 0)
                {
                    return Json(new { success = false, responseText = "Please upload photos / images / documents" });
                }
                else
                {
                    landlordDTO.AttachmentName = new List<string>();

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

                        var path = Path.Combine(env.WebRootPath, "landlorduploads", newFileName);

                        var stream = new FileStream(path, FileMode.Create);

                        photo.CopyTo(stream);

                        landlordDTO.AttachmentName.Add(newFileName);
                    }
                }

                var result = landlordService.Create(landlordDTO);

                if (result != null)
                {

                    var sendsms = await messagingService.LandlordInfo(landlordDTO);

                    return Json(new { success = true, responseText = "Landlord added successfully" });
                }

                else
                {
                    return Json(new { success = false, responseText = "Unable to add Landlord" });
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
