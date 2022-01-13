using System.Collections.Generic;

namespace GroupCCP.Models
{
    public class Priority
    {
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public float PriorityCloseDate { get; set; }
        public ICollection<Timelines> Timelines { get; set; }
        public ICollection<ComplaintLogDetail> ComplaintLogDetails { get; set; }
    }
}
