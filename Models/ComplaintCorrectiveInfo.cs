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
        public int CorrectiveComponentId { get; set; }
        public ComplaintProductComponent ComplaintProductComponent { get; set;}
        public string ComplaintSubComponent { get; set; }
        public ComplaintLogDetail Log { get; set; }
        [Display(Name = "Route Cause Summary")]
        [StringLength(400)]
        public string RouteCause { get; set; }
        [Display(Name = "Corrective Action Summary")]
        [StringLength(400)]
        public string CorrectiveAction { get; set; }
        public string CorrectiveCustomerExplanation { get; set; }
        public int StaffId { get; set; }
        public StaffAccount StaffAccount { get; set; }
        [DataType(DataType.DateTime)]
        public string CorrectiveInfoDate { get; set; }
        public string CorrectiveDiagnosisTimeTaken {get; set; }
        public string CorrectiveRectifyTimeTaken { get; set; }
        public float CorrectivePartsCostKSH { get; set; }
        public float CorrectiveOtherCostKSH { get; set; }

    }
}
