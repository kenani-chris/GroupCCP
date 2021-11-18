using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintCustomerInfo
    {
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        [StringLength(60)]
        public string CustomerName { get; set; }
        [StringLength(14)]
        [Display(Name = "Customerr cell number")]
        public string CustomerCell { get; set; }
        [Display(Name = "Customer Number")]
        public string CustomerNumber { get; set; }
        public ICollection<ComplaintLogDetail> Logs { get; set; }

    }
}
