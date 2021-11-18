using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class FollowUpCalls
    {
        public int FollowUpId { get; set; }
        [StringLength(20)]
        public string FollowUpTime { get; set; }
        [StringLength(20)]
        public string FollowUpType { get; set; }
        public int CompanyId { get; set; }
        public Company  Company { get; set; }
        public ICollection<ComplaintFollowUp> ComplaintFollowUps { get; set; }
    }
}
