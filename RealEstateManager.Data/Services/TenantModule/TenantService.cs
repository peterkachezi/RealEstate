using Microsoft.EntityFrameworkCore;
using RealEstateManager.Data.DTOs.TenantModule;
using RealEstateManager.Data.Helpers;
using RealEstateManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.TenantModule
{
    public class TenantService : ItenantService
    {
        private readonly ApplicationDbContext context;
        public TenantService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public TenantDTO Create(TenantDTO tenantDTO)
        {
            try
            {
                string code = TenantCode.GenerateUniqueNumber();

                tenantDTO.TenantCode = "T" + "" + code;

                tenantDTO.Id = Guid.NewGuid();

                var s = new Tenant
                {
                    Id = tenantDTO.Id,

                    FirstName = tenantDTO.FirstName,

                    LastName = tenantDTO.LastName,

                    PhoneNumber = tenantDTO.PhoneNumber,

                    IdNumber = tenantDTO.IdNumber,

                    Email = tenantDTO.Email,

                    Town = tenantDTO.Town,

                    KinFirstName = tenantDTO.KinFirstName,

                    KinLastName = tenantDTO.KinLastName,

                    KinPhoneNumber = tenantDTO.KinPhoneNumber,

                    KinRelationship = tenantDTO.KinRelationship,

                    CreateDate = DateTime.Now,

                    CreatedBy = tenantDTO.CreatedBy,

                    CountyId = tenantDTO.CountyId,

                    TenantCode = tenantDTO.TenantCode,                                     

                };

                context.Tenants.Add(s);

                var rented = new RentedHouse
                {
                    Id = Guid.NewGuid(),

                    TenantId = tenantDTO.Id,

                    HouseId = tenantDTO.HouseId,

                    ApartmentId = tenantDTO.ApartmentId,

                    CreateDate = DateTime.Now,

                    CreatedBy= tenantDTO.CreatedBy,
                };

                context.RentedHouses.Add(rented);

                foreach (var item in tenantDTO.AttachmentName)
                {
                    var image = new TenantUpload();
                    {
                        image.Id = Guid.NewGuid();

                        image.TenantId = tenantDTO.Id;

                        image.AttachmentName = item;

                        image.CreateDate = DateTime.Now;

                        image.CreatedBy = tenantDTO.CreatedBy;

                    };

                    context.TenantUploads.AddRange(image);
                }

                using(var transaction = context.Database.BeginTransaction())
                {
                    var house = context.Houses.Find(tenantDTO.HouseId);
                    {
                        house.Availability = 1;
                    }
                    transaction.Commit();
                }

                context.SaveChanges();

                return tenantDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public Task<TenantDTO> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TenantDTO>> GetAll()
        {
            try
            {
                var tenants = (from t in context.Tenants

                               join u in context.AppUser on t.CreatedBy equals u.Id

                               join c in context.Counties on t.CountyId equals c.Id

                               //join h in context.Houses on t.HouseId equals h.Id

                               //join a in context.Apartments on h.ApartmentId equals a.Id

                               //join l in context.Landlords on a.LandlordId equals l.Id

                               select new TenantDTO
                               {
                                   Id = t.Id,

                                   FirstName = t.FirstName,

                                   LastName = t.LastName,

                                   PhoneNumber = t.PhoneNumber,

                                   IdNumber = t.IdNumber,

                                   Email = t.Email,

                                   Town = t.Town,

                                   KinFirstName = t.KinFirstName,

                                   KinLastName = t.KinLastName,

                                   KinPhoneNumber = t.PhoneNumber,

                                   CreateDate = t.CreateDate,

                                   CreatedBy = t.CreatedBy,

                                   CountyId = t.CountyId,

                                   CreatedByName = u.FirstName + " " + u.LastName,

                                   CountyName = c.Name,

                                   TenantCode = t.TenantCode,

                                   //HouseId = t.HouseId,

                                   //HouseName = h.Name,

                                   //ApartmentName = a.Name,

                                   //LandlordName = l.FirstName + " " + l.LastName,

                                   //LandlordEmail = l.Email,

                                   //LandlordPhoneNumber = l.PhoneNumber,

                               }).ToListAsync();

                return await tenants;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public Task<TenantDTO> Update(TenantDTO tenantDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.Tenants.FindAsync(Id);

                if (s != null)
                {
                    context.Tenants.Remove(s);

                    await context.SaveChangesAsync();

                    return true;
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }
    }
}
