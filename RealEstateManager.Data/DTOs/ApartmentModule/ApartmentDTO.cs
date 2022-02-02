using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.DTOs.ApartmentModule
{
    public class ApartmentDTO
    {
        public Guid Id { get; set; }
        public Guid LandlordId { get; set; }
        public string LandlordName { get; set; }
        public string Name { get; set; }
        public string Estate { get; set; }
        public string PhysicalAddress { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public string Town { get; set; }
        public DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }
}
