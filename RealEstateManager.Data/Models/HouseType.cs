using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstateManager.Data.Models
{
   public class HouseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }
    }
}
