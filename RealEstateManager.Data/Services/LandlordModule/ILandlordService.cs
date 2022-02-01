using RealEstateManager.Data.DTOs.LandlordModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.LandlordModule
{
    public interface ILandlordService
    {
        Task<LandlordDTO> Create(LandlordDTO landlordDTO);
        Task<LandlordDTO> Update(LandlordDTO landlordDTO);
        Task<bool> Delete(Guid Id);
        Task<LandlordDTO> GetById(Guid Id);
        Task<List<LandlordDTO>> GetAll();
     
    }
}