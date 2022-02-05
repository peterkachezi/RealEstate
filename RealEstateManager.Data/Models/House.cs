using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstateManager.Data.Models
{
    public class House
    {
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid HouseTypeId { get; set; }
        public string Name { get; set; }
        public byte Availability { get; set; }
        public string Condition { get; set; }
        public decimal RentAmount { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }
    }
}
