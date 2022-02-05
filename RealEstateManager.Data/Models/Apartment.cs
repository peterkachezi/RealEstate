using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstateManager.Data.Models
{
    public class Apartment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Estate { get; set; }
        public string PhysicalAddress { get; set; }
        public int CountyId { get; set; }
        public string Town { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }

        public Guid LandlordId { get; set; }
        public Landlord Landlord { get; set; }
    }
}
