using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintLogDetail
    {
        public int LogId { get; set; }
        [Display(Name = "Customer")]
        public int LogCustomerId { get; set; }
        public ComplaintCustomerInfo Customers { get; set; }
        [StringLength(500)]
        [Display(Name ="Customer Complaint")] 
        public string CustomerComplaint { get; set; }
        [StringLength(500)]
        [Display(Name = "Customer Request")]
        public string CustomerRequest { get; set; }
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }
        public int BrandId { get; set; }
        public Brands Brands { get; set; }
        public string Model { get; set; }
        [Display(Name = "Receive Means")]
        public int LogMeansId { get; set; } 
        public ComplaintReceiveMeans Means { get; set; }
        [Display(Name = "Level")]
        public int LogLevelId { get; set; }
        public Level Level { get; set; }
        [Display(Name = "Status")]
        public int LogStatusId { get; set; }
        public  ComplaintLogStatus Status { get; set; }
        public int StaffId { get; set; }
        public StaffAccount StaffAccount { get; set; }
        [Display(Name = "Log Submit Date")]
        [DataType(DataType.DateTime)]
        public string StatusSubmitDate { get; set; }
        [Display(Name = "Log Close Date")]
        [DataType(DataType.DateTime)]
        public string StatusClosedDate { get; set; }
        
        public ICollection<ComplaintAssignment> Assignments { get; set; }
        public ICollection<ComplaintCorrectiveInfo> Correctives { get; set; }
        public ICollection<ComplaintFollowUp> ComplaintFollowUps { get; set; }
    }
}
