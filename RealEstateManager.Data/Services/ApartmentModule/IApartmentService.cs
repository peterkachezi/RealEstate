using RealEstateManager.Data.DTOs.ApartmentModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.ApartmentModule
{
    public interface IApartmentService
    {
        Task<ApartmentDTO> Create(ApartmentDTO apartmentDTO);
        Task<ApartmentDTO> Update(ApartmentDTO apartmentDTO);
        Task<bool> Delete(Guid Id);
        Task<ApartmentDTO> GetById(Guid Id);
        Task<List<ApartmentDTO>> GetAll();
    }
}