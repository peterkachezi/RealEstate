using RealEstateManager.Data.DTOs.HouseTypeModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.HouseTypeModule
{
    public interface IHouseTypeService
    {
        Task<HouseTypeDTO> Create(HouseTypeDTO houseTypeDTO);
        Task<HouseTypeDTO> Update(HouseTypeDTO houseTypeDTO);
        Task<List<HouseTypeDTO>> GetAll();
        Task<HouseTypeDTO> GetById(Guid Id);
        Task<bool> Delete(Guid Id);
    }
}