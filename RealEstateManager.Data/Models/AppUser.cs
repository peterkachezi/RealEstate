using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgentId { get; set; }
        public string MemberId { get; set; }
        public int Commissions { get; set; }
        public bool isActive { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
