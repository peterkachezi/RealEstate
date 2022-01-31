using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstateManager.Data.Models
{
   public class County
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

    }
}
