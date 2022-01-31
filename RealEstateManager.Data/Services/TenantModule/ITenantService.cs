using RealEstateManager.Data.DTOs.TenantModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.TenantModule
{
    public interface ItenantService
    {
        Task<TenantDTO> Create(TenantDTO tenantDTO);
        Task<TenantDTO> Update(TenantDTO tenantDTO);
        Task<List<TenantDTO>> GetAll();
        Task<TenantDTO> GetById(Guid Id);
    }
}