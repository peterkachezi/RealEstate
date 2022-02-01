using Microsoft.EntityFrameworkCore;
using RealEstateManager.Data.DTOs.TenantModule;
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
        public async Task<TenantDTO> Create(TenantDTO tenantDTO)
        {
            try
            {
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

                    CreateDate = DateTime.Now,

                    CreatedBy = tenantDTO.CreatedBy,

                    CountyId = tenantDTO.CountyId

                };

                context.Tenants.Add(s);

                //var myimage = tenantDTO.AttachmentName.ToList();
              
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
                    context.TenantUploads.Add(image);
                }

                await context.SaveChangesAsync();

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

                                   CreatedByName = u.FirstName +" " + u.LastName,

                                   CountyName = c.Name,

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
    }
}
