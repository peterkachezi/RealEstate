using RealEstateManager.Data.DTOs.ApplicationUsersModule;

namespace RealEstateManager.EmailServiceModule
{
    public interface IEmailService
    {
        public bool SendAccountCreationEmailNotification(RegisterDTO registerDTO);
  
    }
}