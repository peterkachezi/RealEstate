using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.Models
{
    public class Landlord 
    {
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PlotLocation { get; set; }
        public string BankAccountNo{ get; set; }
        public string KinName{ get; set; }
        public string KinPhoneNumber{ get; set; }
        public string KinRelationship{ get; set; }
        public string AttachmentName { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
