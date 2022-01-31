using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RealEstateManager.Data.Models
{ 

    public partial class TenantUpload
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        [Required]
        public string AttachmentName { get; set; }

        [Required]
        [StringLength(450)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

    }
}
