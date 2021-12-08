using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class StaffAccount
    {
        public int AccountId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Is SuperUser")]
        public bool IsSuperUser { get; set; }
        [DataType(DataType.DateTime)]
        public string CreateDate { get; set; }
        public ICollection<ComplaintAssignment> Assignments { get; set; }
        public ICollection<RoleAssignment> RolesAssignments { get; set; }
        public ICollection<ComplaintLogDetail> ComplaintLogDetails { get; set; }
        public ICollection<ComplaintCorrectiveInfo> ComplaintCorrectiveInfos { get; set; }
    }
}
