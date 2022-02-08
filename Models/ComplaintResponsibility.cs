using System.Collections.Generic;

namespace GroupCCP.Models
{
    public class ComplaintResponsibility
    {
        public int ResponsibilityId { get; set; }
        public int LogId { get; set; }
        public ComplaintLogDetail Log { get; set; }
        public string ResponsibilityPIC { get; set; }
        public string ResponsibilityLevel { get; set; }
        public string ResponsibilityReason { get; set; }
    }
}
