using RealEstateManager.Data.DTOs.TenantModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.TenantModule
{
    public interface ItenantService
    {
        TenantDTO Create(TenantDTO tenantDTO);
        Task<TenantDTO> Update(TenantDTO tenantDTO);
        Task<List<TenantDTO>> GetAll();
        Task<TenantDTO> GetById(Guid Id);
        Task<bool> Delete(Guid Id);
    }
}