using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.DTOs.HouseModule
{
    public class HouseDTO
    {
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public Guid HouseTypeId { get; set; }
        public string HouseTypeName { get; set; }
        public string Name { get; set; }
        public byte Availability { get; set; }
        public string AvailabilityDescription { get; set; }
        public string Condition { get; set; }
        public decimal RentAmount { get; set; }
        public string NewRentAmount { get { return RentAmount.ToString(); } }
        public DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }
}
