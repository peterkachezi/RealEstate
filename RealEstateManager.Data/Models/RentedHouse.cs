using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstateManager.Data.Models
{
    public class RentedHouse
    {
        public Guid  Id { get; set; }
        public Guid  TenantId { get; set; }
        public Guid  HouseId { get; set; }
        public Guid  ApartmentId { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }
    }
}
