using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintLogStatus
    {
        public int StatusId { get; set; }
        [Required]
        public string Status { get; set; }

        public ICollection<ComplaintLogDetail> Logs { get; set; }
    }
}
