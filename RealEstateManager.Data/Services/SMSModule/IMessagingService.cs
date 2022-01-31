using RealEstateManager.Data.DTOs.ApplicationUsersModule;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.SMSModule
{
    public interface IMessagingService
    {
        Task<RegisterDTO> usersAccount(RegisterDTO registerDTO);
    }
}