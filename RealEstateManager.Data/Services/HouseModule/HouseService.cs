using RealEstateManager.Data.DTOs.HouseModule;
using RealEstateManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RealEstateManager.Data.Services.HouseModule
{
    public class HouseService : IHouseService
    {
        private readonly ApplicationDbContext context;

        public HouseService(ApplicationDbContext context)
        {
            this.context = context;
        }






        public async Task<HouseDTO> Create(HouseDTO houseDTO)
        {
            try
            {
                var s = new House
                {
                    Id = Guid.NewGuid(),

                    ApartmentId = houseDTO.ApartmentId,

                    HouseTypeId = houseDTO.HouseTypeId,

                    Name = houseDTO.Name,

                    Availability = houseDTO.Availability,

                    Condition = houseDTO.Condition,

                    RentAmount = houseDTO.RentAmount,

                    CreateDate = DateTime.Now,

                    CreatedBy = houseDTO.CreatedBy,
                };

                context.Houses.Add(s);

                await context.SaveChangesAsync();

                return houseDTO;
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

                var s = await context.Houses.FindAsync(Id);

                if (s != null)
                {
                    context.Houses.Remove(s);

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

        public async Task<List<HouseDTO>> GetAll()
        {
            try
            {
                var houses = (from h in context.Houses

                              join aptmnt in context.Apartments on h.ApartmentId equals aptmnt.Id

                              join u in context.AppUser on h.CreatedBy equals u.Id

                              join ht in context.HouseTypes on h.HouseTypeId equals ht.Id


                              select new HouseDTO
                              {
                                  Id = h.Id,

                                  ApartmentId = h.ApartmentId,

                                  ApartmentName = aptmnt.Name,

                                  HouseTypeId = h.HouseTypeId,

                                  HouseTypeName = ht.Name,

                                  Availability = h.Availability,

                                  Name = h.Name,

                                  Condition = h.Condition,

                                  RentAmount = h.RentAmount,

                                  CreateDate = h.CreateDate,

                                  CreatedBy = h.CreatedBy,

                                  CreatedByName = u.FirstName + " " + u.LastName,

                              }).ToListAsync();

                return await houses;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<HouseDTO> GetById(Guid Id)
        {
            try
            {
                var houses = (from h in context.Houses

                              join aptmnt in context.Apartments on h.ApartmentId equals aptmnt.Id

                              join u in context.AppUser on h.CreatedBy equals u.Id

                              where h.Id == Id

                              select new HouseDTO
                              {
                                  Id = h.Id,

                                  ApartmentId = h.ApartmentId,

                                  ApartmentName = aptmnt.Name,

                                  HouseTypeId = h.HouseTypeId,

                                  Availability = h.Availability,

                                  Name = h.Name,

                                  Condition = h.Condition,

                                  RentAmount = h.RentAmount,

                                  CreateDate = h.CreateDate,

                                  CreatedBy = h.CreatedBy,

                                  CreatedByName = u.FirstName + " " + u.LastName,

                              }).FirstAsync();

                return await houses;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<HouseDTO> Update(HouseDTO houseDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Houses.FindAsync(houseDTO.Id);
                    {
                        s.ApartmentId = houseDTO.ApartmentId;

                        s.HouseTypeId = houseDTO.HouseTypeId;

                        s.Name = houseDTO.Name;

                        s.Availability = houseDTO.Availability;

                        s.Condition = houseDTO.Condition;

                        s.RentAmount = houseDTO.RentAmount;

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return houseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
