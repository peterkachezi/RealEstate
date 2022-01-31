using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string AttachmentName { get; set; }
        public string KinName { get; set; }
        public string KinPhoneNumber { get; set; }
        public string KinRelationship { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
