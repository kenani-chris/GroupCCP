using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintVehicleInfo
    {
        public int VehicleId { get; set; }
        public int LogId { get; set; }
        public ComplaintLogDetail Logs { get; set; }
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }
        public int BrandId { get; set; }
        public Brands Brands { get; set; }
    }
}
