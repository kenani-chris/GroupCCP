using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class Permissions
    {
        public int PermissionId { get; set; }
        public string Entity { get; set; }
        public string Permission { get; set; }

        [NotMapped]
        public string PermissionEntity
        {
            get
            {
                return Entity + " - " + Permission;
            }
        }

        public ICollection<PermissionAssignment> Assignments { get; set; }
    }
}
