using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
