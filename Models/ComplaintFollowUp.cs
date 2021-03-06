using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintFollowUp
    {
        public int FollowUpId { get; set; }
        public int LogId { get; set; }
        public ComplaintLogDetail Log { get; set; }
        public int StaffId { get; set; }
        public StaffAccount Staff { get; set; }
        public int FollowUpTypeId { get; set; }
        public FollowUpCalls FollowUpCalls { get; set; }
        [DataType(DataType.DateTime)]
        public string FollowUpDate { get; set; }
        public string FollowUpFeedback { get; set; }
    }
}