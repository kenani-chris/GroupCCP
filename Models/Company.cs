using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        [StringLength(30)]
        public string CompanyName { get; set; }
        [StringLength(10)]
        public string CompanyNameAbbreviation { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Company Enrollment Date")]
        public string CompanyCreateDate { get; set; }
        public bool CompanyActive { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<LevelCategory> LevelCategories { get; set; }
        public ICollection<Roles> Roles { get; set; }
        public ICollection<Brands> Brands { get; set; }
        public ICollection<FollowUpCalls> FollowUps { get; set; }
        public ICollection<ComplaintCustomerInfo> Customers { get; set; }
    }
}
