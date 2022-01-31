using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.DTOs.TenantModule
{
    public class TenantDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string AttachmentName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KinName { get; set; }
        public string KinPhoneNumber { get; set; }
        public string KinRelationship { get; set; }
    }
}
