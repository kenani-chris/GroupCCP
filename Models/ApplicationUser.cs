using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<StaffAccount> StaffAccounts { get; set; }
    }
}
