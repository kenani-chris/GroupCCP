using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class PermissionAssignment
    {
        public int AssignmentId { get; set; }
        public int RoleId { get; set; }
        public Roles Roles { get; set; }

        public int PermissionId { get; set; }
        public Permissions Permissions { get; set; }
    }
}
