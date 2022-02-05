using RealEstateManager.Data.DTOs.ApplicationUsersModule;
using RealEstateManager.Data.DTOs.LandlordModule;
using RealEstateManager.Data.DTOs.TenantModule;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.SMSModule
{
    public interface IMessagingService
    {
        Task<RegisterDTO> usersAccount(RegisterDTO registerDTO);
        Task<TenantDTO> TenantInfo(TenantDTO tenantDTO);
        Task<LandlordDTO> LandlordInfo(LandlordDTO landlordDTO);
    }
}