using Microsoft.Extensions.Configuration;
using RealEstateManager.Data.DTOs.ApplicationUsersModule;
using RealEstateManager.Data.DTOs.LandlordModule;
using RealEstateManager.Data.DTOs.TenantModule;
using RealEstateManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManager.Data.Services.SMSModule
{
    public class MessagingService : IMessagingService
    {
        private readonly IConfiguration config;

        private readonly ApplicationDbContext  context;
        public MessagingService(ApplicationDbContext context,IConfiguration config)
        {
            this.config = config;

            this.context = context;
        }
        public async Task<RegisterDTO> usersAccount(RegisterDTO registerDTO)
        {
            try
            {
                var url = "http://167.172.14.50:4002/v1/send-sms";

                var txtMessage = "Dear  " + registerDTO.FirstName + " Welcome to I & P Motors , one of our agents will attend to you shortly. Helpline :+254 729 884 569";

                var key = config.GetValue<string>("SMS_Settings:BongaSMSKey");

                var secrete = config.GetValue<string>("SMS_Settings:BongaSMSSecrete");

                var apiClientID = config.GetValue<string>("SMS_Settings:BongaSMSApiClientID");

                var serviceID = config.GetValue<string>("SMS_Settings:BongaSMSServiceID");

                var msisdn = formatPhoneNumber(registerDTO.PhoneNumber);

                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("apiClientID", apiClientID),
                new KeyValuePair<string, string>("secret", secrete),
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("txtMessage", txtMessage),
                new KeyValuePair<string, string>("MSISDN", msisdn),
                new KeyValuePair<string, string>("serviceID", serviceID),
                new KeyValuePair<string, string>("enqueue", "yes"),
            });

                HttpClient client = new HttpClient();

                HttpResponseMessage apiResult = await client.PostAsync(url, formContent);

                apiResult.EnsureSuccessStatusCode();

                var response = await apiResult.Content.ReadAsStringAsync();


                //return new Tuple<bool, TextAlertDTO, string>(true, textAlertDTO, "Text Alert sent successfully!");

                return registerDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }


        public async Task<TenantDTO> TenantInfo(TenantDTO tenantDTO)
        {
            try
            {
                var get_house_details = await context.Houses.FindAsync(tenantDTO.HouseId);

                var url = "http://167.172.14.50:4002/v1/send-sms";

                var txtMessage = "Dear  " + tenantDTO.FirstName + " Welcome to Sulphur Spot Solutions .You have been allocated house no." + get_house_details.Name + ".Your Tenant code is :" + tenantDTO.TenantCode + ". For any inquries please reach us through our Helpline No.:+254700030220";

                var key = config.GetValue<string>("SMS_Settings:BongaSMSKey");

                var secrete = config.GetValue<string>("SMS_Settings:BongaSMSSecrete");

                var apiClientID = config.GetValue<string>("SMS_Settings:BongaSMSApiClientID");

                var serviceID = config.GetValue<string>("SMS_Settings:BongaSMSServiceID");

                var msisdn = formatPhoneNumber(tenantDTO.PhoneNumber);

                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("apiClientID", apiClientID),
                new KeyValuePair<string, string>("secret", secrete),
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("txtMessage", txtMessage),
                new KeyValuePair<string, string>("MSISDN", msisdn),
                new KeyValuePair<string, string>("serviceID", serviceID),
                new KeyValuePair<string, string>("enqueue", "yes"),
            });

                HttpClient client = new HttpClient();

                HttpResponseMessage apiResult = await client.PostAsync(url, formContent);

                apiResult.EnsureSuccessStatusCode();

                var response = await apiResult.Content.ReadAsStringAsync();


                //return new Tuple<bool, TextAlertDTO, string>(true, textAlertDTO, "Text Alert sent successfully!");

                return tenantDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }


        public string formatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            string formatted = "";

            if (phoneNumber.StartsWith("0"))
                formatted = "+254" + phoneNumber.Substring(1, phoneNumber.Length - 1);

            if (phoneNumber.StartsWith("7"))
                formatted = "+254" + phoneNumber;

            if (phoneNumber.StartsWith("+254"))
                formatted = phoneNumber;

            if (phoneNumber.StartsWith("254"))
                formatted = "+" + phoneNumber;

            return formatted;
        }

        public async Task<LandlordDTO> LandlordInfo(LandlordDTO landlordDTO)
        {
            try
            {               

                var url = "http://167.172.14.50:4002/v1/send-sms";

                var txtMessage = "Dear  " + landlordDTO.FirstName + " " +
                    "you have been successfully registered with Sulphur " +
                    "Spot Solutions as Landlord .Your landlord code is :" + landlordDTO.LandlordCode + "." +
                    " For any inquries please reach us through our Helpline No.:+254700030220";

                var key = config.GetValue<string>("SMS_Settings:BongaSMSKey");

                var secrete = config.GetValue<string>("SMS_Settings:BongaSMSSecrete");

                var apiClientID = config.GetValue<string>("SMS_Settings:BongaSMSApiClientID");

                var serviceID = config.GetValue<string>("SMS_Settings:BongaSMSServiceID");

                var msisdn = formatPhoneNumber(landlordDTO.PhoneNumber);

                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("apiClientID", apiClientID),
                new KeyValuePair<string, string>("secret", secrete),
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("txtMessage", txtMessage),
                new KeyValuePair<string, string>("MSISDN", msisdn),
                new KeyValuePair<string, string>("serviceID", serviceID),
                new KeyValuePair<string, string>("enqueue", "yes"),
            });

                HttpClient client = new HttpClient();

                HttpResponseMessage apiResult = await client.PostAsync(url, formContent);

                apiResult.EnsureSuccessStatusCode();

                var response = await apiResult.Content.ReadAsStringAsync();


                //return new Tuple<bool, TextAlertDTO, string>(true, textAlertDTO, "Text Alert sent successfully!");

                return landlordDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
