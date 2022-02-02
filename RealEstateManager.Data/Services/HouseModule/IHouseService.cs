using RealEstateManager.Data.DTOs.HouseModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.HouseModule
{
    public interface IHouseService
    {
        Task<HouseDTO> Create(HouseDTO houseDTO);
        Task<HouseDTO> Update(HouseDTO houseDTO);
        Task<bool> Delete(Guid Id);
        Task<HouseDTO> GetById(Guid Id);
        Task<List<HouseDTO>> GetAll();
    }
}