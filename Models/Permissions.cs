using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class Permissions
    {
        public int PermissionId { get; set; }
        [StringLength(20)]
        public string Entity { get; set; }
        public string Permission { get; set; }

        public ICollection<PermissionAssignment> Assignments { get; set; }
    }
}
