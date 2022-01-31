using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.DTOs.HouseModule
{
    public class HouseDTO
    {
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
