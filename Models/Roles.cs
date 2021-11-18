using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class Roles
    {
        public int RoleId { get; set; }
        [StringLength(20)]
        public string Role { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<PermissionAssignment> Assignments { get; set; }
        public ICollection<RoleAssignment> RoleAssignments { get; set; }
    }
}
