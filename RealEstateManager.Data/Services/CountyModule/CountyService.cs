using Microsoft.EntityFrameworkCore;
using RealEstateManager.Data.DTOs.CountyModule;
using RealEstateManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.CountyModule
{
    public class CountyService : ICountyService
    {
        private readonly ApplicationDbContext context;

        public CountyService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<CountyDTO>> GetAll()
        {
            try
            {
                var county = await context.Counties.ToListAsync();

                var counties = new List<CountyDTO>();

                foreach (var item in county)
                {
                    var data = new CountyDTO
                    {
                        Id = item.Id,

                        Name = item.Name,
                    };

                    counties.Add(data);
                }

                return counties;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
