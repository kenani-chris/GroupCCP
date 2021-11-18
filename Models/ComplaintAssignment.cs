using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintAssignment
    {
        public int AssignmentId { get; set; }
        public int StaffAssigned { get; set; }
        public StaffAccount Staff { get; set; }

        public int LogId { get; set; }
        public ComplaintLogDetail Log { get; set; }
    }
}
