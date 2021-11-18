﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class ComplaintFollowUp
    {
        public int FollowUpId { get; set; }
        public int LogId { get; set; }
        public ComplaintLogDetail Logs { get; set; }
        public int StaffId { get; set; }
        public StaffAccount Staff { get; set; }
        public int FollowUp { get; set; }
        public FollowUpCalls FollowUps { get; set; }

    }
}