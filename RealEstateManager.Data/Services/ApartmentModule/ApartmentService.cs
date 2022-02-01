using RealEstateManager.Data.DTOs.ApartmentModule;
using RealEstateManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RealEstateManager.Data.Services.ApartmentModule
{
    public class ApartmentService : IApartmentService
    {
        private readonly ApplicationDbContext context;

        public ApartmentService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<ApartmentDTO> Create(ApartmentDTO apartmentDTO)
        {
            try
            {
                var s = new Apartment
                {
                    Id = Guid.NewGuid(),

                    LandlordId = apartmentDTO.LandlordId,

                    CountyId = apartmentDTO.CountyId,

                    Name = apartmentDTO.Name,

                    Town = apartmentDTO.Town,

                    CreateDate = DateTime.Now,

                    CreatedBy = apartmentDTO.CreatedBy,
                };

                context.Apartments.Add(s);

                await context.SaveChangesAsync();

                return apartmentDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.Apartments.FindAsync(Id);

                if (s != null)
                {
                    context.Apartments.Remove(s);

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

        public async Task<List<ApartmentDTO>> GetAll()
        {
            try
            {
                var apartment = (from a in context.Apartments

                                 join l in context.Landlords on a.LandlordId equals l.Id

                                 join u in context.AppUser on a.CreatedBy equals u.Id

                                 select new ApartmentDTO
                                 {
                                     Id = a.Id,

                                     LandlordId = a.LandlordId,

                                     CountyId = a.CountyId,

                                     Name = a.Name,

                                     Town = a.Town,

                                     CreateDate = a.CreateDate,

                                     CreatedBy = a.CreatedBy,

                                     CreatedByName = u.FirstName + " " + u.LastName,

                                     LandlordName = l.FirstName + " " + l.LastName,
                                 }

                                 ).ToListAsync();

                return await apartment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ApartmentDTO> GetById(Guid Id)
        {
            try
            {
                var apartment = (from a in context.Apartments

                                 join l in context.Landlords on a.LandlordId equals l.Id

                                 join u in context.AppUser on a.CreatedBy equals u.Id

                                 where a.Id == Id

                                 select new ApartmentDTO
                                 {
                                     Id = a.Id,

                                     LandlordId = a.LandlordId,

                                     CountyId = a.CountyId,

                                     Name = a.Name,

                                     Town = a.Town,

                                     CreateDate = a.CreateDate,

                                     CreatedBy = a.CreatedBy,

                                     CreatedByName = u.FirstName + " " + u.LastName,

                                     LandlordName = l.FirstName + " " + l.LastName,
                                 }

                                 ).FirstOrDefaultAsync();

                return await apartment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ApartmentDTO> Update(ApartmentDTO apartmentDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Apartments.FindAsync();
                    {
                        s.LandlordId = apartmentDTO.LandlordId;

                        s.CountyId = apartmentDTO.CountyId;

                        s.Name = apartmentDTO.Name;

                        s.Town = apartmentDTO.Town;
                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }
                return apartmentDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
