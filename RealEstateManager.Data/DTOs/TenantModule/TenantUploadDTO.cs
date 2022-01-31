using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.DTOs.TenantModule
{
    public class TenantUploadDTO
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string AttachmentName { get; set; }
        public string CreatedBy { get; set; }  
        public DateTime CreateDate { get; set; }
    }
}
