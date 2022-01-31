using RealEstateManager.Data.DTOs.CountyModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.CountyModule
{
    public interface ICountyService
    {
        Task<List<CountyDTO>> GetAll();
    }
}