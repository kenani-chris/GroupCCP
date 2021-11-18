using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintCorrectiveInfo
    {
        public int CorrectiveId { get; set; }
        public int LogId { get; set; }
        public ComplaintLogDetail Log { get; set; }
        [Display(Name = "Route Cause Summary")]
        [StringLength(400)]
        public string RouteCause { get; set; }
        [Display(Name = "Corrective Action Summary")]
        [StringLength(400)]
        public string CorrectiveAction { get; set; }
    }
}
