using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintCustomerInfo
    {
        [NotMapped]
        public string Customer
        {
            get
            {
                return CustomerName + " - " + CustomerNumber;
            }
        }
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name")]
        [StringLength(60)]
        public string CustomerName { get; set; }
        [StringLength(14)]
        [Display(Name = "Customerr cell number")]
        public string CustomerCell { get; set; }
        [Display(Name = "Customer Number")]
        public string CustomerNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerOccupation { get; set; }
        public string CustomerCompany { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<ComplaintLogDetail> Logs { get; set; }

    }
}
