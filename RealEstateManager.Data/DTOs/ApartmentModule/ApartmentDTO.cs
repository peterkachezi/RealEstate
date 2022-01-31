using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.DTOs.ApartmentModule
{
    public class ApartmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
