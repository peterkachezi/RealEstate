using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.DTOs.LandlordModule
{
    public class LandlordDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public string Town { get; set; }
        public int CountyId { get; set; }
        public string BankAccountNo { get; set; }
        public string KinLastName { get; set; }
        public string KinFirstName { get; set; }
        public string KinName { get; set; }
        public string KinPhoneNumber { get; set; }
        public string KinRelationship { get; set; }
        public string AttachmentName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }
}
