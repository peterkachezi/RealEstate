using RealEstateManager.Data.DTOs.LandlordModule;
using RealEstateManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RealEstateManager.Data.Services.LandlordModule
{
    public class LandlordService : ILandlordService
    {
        private readonly ApplicationDbContext context;

        public LandlordService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<LandlordDTO> Create(LandlordDTO landlordDTO)
        {
            try
            {
                var s = new Landlord
                {
                    Id = Guid.NewGuid(),

                    FirstName = landlordDTO.FirstName,

                    LastName = landlordDTO.LastName,

                    PhoneNumber = landlordDTO.PhoneNumber,

                    Email = landlordDTO.Email,

                    IdNumber = landlordDTO.IdNumber,

                    Town = landlordDTO.Town,

                    CountyId = landlordDTO.CountyId,

                    BankAccountNo = landlordDTO.BankAccountNo,

                    KinFirstName = landlordDTO.KinFirstName,

                    KinLastName = landlordDTO.KinLastName,

                    KinPhoneNumber = landlordDTO.KinPhoneNumber,

                    KinRelationship = landlordDTO.KinRelationship,

                    CreateDate = DateTime.Now,

                    CreatedBy = landlordDTO.CreatedBy,

                };

                context.Landlords.Add(s);

                await context.SaveChangesAsync();

                return landlordDTO;
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

                var s = await context.Landlords.FindAsync(Id);

                if (s != null)
                {
                    context.Landlords.Remove(s);

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

        public async Task<List<LandlordDTO>> GetAll()
        {
            try
            {
                var landloards = (from l in context.Landlords

                                  join u in context.AppUser on l.CreatedBy equals u.Id

                                  select new LandlordDTO
                                  {
                                      Id = l.Id,

                                      FirstName = l.FirstName,

                                      LastName = l.LastName,

                                      PhoneNumber = l.PhoneNumber,

                                      Town = l.Town,

                                      Email = l.Email,

                                      IdNumber = l.IdNumber,

                                      CountyId = l.CountyId,

                                      BankAccountNo = l.BankAccountNo,

                                      KinFirstName = l.KinFirstName,

                                      KinLastName = l.KinLastName,

                                      KinPhoneNumber = l.KinPhoneNumber,

                                      KinRelationship = l.KinRelationship,

                                      CreateDate = l.CreateDate,

                                      CreatedBy = l.CreatedBy,

                                      CreatedByName = u.FirstName + " " + u.LastName,
                                  }

                                  ).ToListAsync();

                return await landloards;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<LandlordDTO> GetById(Guid Id)
        {
            try
            {
                var landloards = (from l in context.Landlords

                                  join u in context.AppUser on l.CreatedBy equals u.Id

                                  where l.Id == Id

                                  select new LandlordDTO
                                  {
                                      Id = l.Id,

                                      FirstName = l.FirstName,

                                      LastName = l.LastName,

                                      PhoneNumber = l.PhoneNumber,

                                      Town = l.Town,

                                      Email = l.Email,

                                      IdNumber = l.IdNumber,

                                      CountyId = l.CountyId,

                                      BankAccountNo = l.BankAccountNo,

                                      KinFirstName = l.KinFirstName,

                                      KinLastName = l.KinLastName,

                                      KinPhoneNumber = l.KinPhoneNumber,

                                      KinRelationship = l.KinRelationship,

                                      CreateDate = l.CreateDate,

                                      CreatedBy = l.CreatedBy,

                                      CreatedByName = u.FirstName + " " + u.LastName,
                                  }

                                  ).FirstOrDefaultAsync();

                return await landloards;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<LandlordDTO> Update(LandlordDTO landlordDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    var s = await context.Landlords.FindAsync(landlordDTO.Id);
                    {
                        s.FirstName = landlordDTO.FirstName;

                        s.LastName = landlordDTO.LastName;

                        s.PhoneNumber = landlordDTO.PhoneNumber;

                        s.Town = landlordDTO.Town;

                        s.Email = landlordDTO.Email;

                        s.IdNumber = landlordDTO.IdNumber;

                        s.CountyId = landlordDTO.CountyId;

                        s.BankAccountNo = landlordDTO.BankAccountNo;

                        s.KinFirstName = landlordDTO.KinFirstName;

                        s.KinLastName = landlordDTO.KinLastName;

                        s.KinPhoneNumber = landlordDTO.KinPhoneNumber;

                        s.KinRelationship = landlordDTO.KinRelationship;

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return landlordDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
