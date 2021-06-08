using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime RegisteringDate { get; set; }
        public DateTime LastSigningDate { get; set;  }
        public string Status { get; set; }
        public bool isChecked { get; set; } = false;
    }
}
