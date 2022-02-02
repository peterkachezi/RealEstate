using RealEstateManager.Data.DTOs.HouseTypeModule;
using RealEstateManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RealEstateManager.Data.Services.HouseTypeModule
{
    public class HouseTypeService : IHouseTypeService
    {
        private readonly ApplicationDbContext context;

        public HouseTypeService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<HouseTypeDTO> Create(HouseTypeDTO houseTypeDTO)
        {
            try
            {
                var s = new HouseType
                {
                    Id = Guid.NewGuid(),

                    Name = houseTypeDTO.Name,

                    CreateDate = DateTime.Now,

                    CreatedBy = houseTypeDTO.CreatedBy,
                };

                context.HouseTypes.Add(s);

                await context.SaveChangesAsync();

                return houseTypeDTO;

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

                var s = await context.HouseTypes.FindAsync(Id);

                if (s != null)
                {
                    context.HouseTypes.Remove(s);

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

        public Task<List<HouseTypeDTO>> GetAll()
        {
            try
            {
                var housetypes = (from h in context.HouseTypes

                                  join u in context.AppUser on h.CreatedBy equals u.Id

                                  select new HouseTypeDTO
                                  {
                                      Id = h.Id,

                                      Name = h.Name,

                                      CreateDate = h.CreateDate,

                                      CreatedBy = h.CreatedBy,

                                      CreatedByName = u.FirstName + " " + u.LastName,
                                  }

                                    ).ToListAsync();

                return housetypes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public Task<HouseTypeDTO> GetById(Guid Id)
        {
            try
            {
                var housetypes = (from h in context.HouseTypes

                                  join u in context.AppUser on h.CreatedBy equals u.Id

                                  where h.Id == Id

                                  select new HouseTypeDTO
                                  {
                                      Id = h.Id,

                                      Name = h.Name,

                                      CreateDate = h.CreateDate,

                                      CreatedBy = h.CreatedBy,

                                      CreatedByName = u.FirstName + " " + u.LastName,

                                  }).FirstOrDefaultAsync();


                return housetypes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<HouseTypeDTO> Update(HouseTypeDTO houseTypeDTO)
        {
            try
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.HouseTypes.FindAsync(houseTypeDTO.Id);
                    {
                        s.Name = houseTypeDTO.Name;

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }               

                return houseTypeDTO;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
