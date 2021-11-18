using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintReceiveMeans
    {
        public int MeansId { get; set; }
        [Required]
        [StringLength(10)]
        [Display(Name = "Receive Means")]
        public string Means { get; set; }
        public ICollection<ComplaintLogDetail> Logs { get; set; }
    }
}
