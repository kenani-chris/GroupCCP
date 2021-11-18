using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class RoleAssignment
    {
        public int RoleAssignmentId { get; set; }
        public int StaffId { get; set; }
        public StaffAccount Staff { get; set; }
        public int RoleID { get; set; }
        public Roles Roles { get; set; }
    }
}
