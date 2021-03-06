using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual IEnumerable<StaffAccount> StaffAccounts { get; set; }
    }
}
