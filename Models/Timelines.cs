namespace GroupCCP.Models
{
    public class Timelines
    {
        public int TimeLineId { get; set; } = 1;
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
        public float OverdueAssignedHrs { get; set; }
        public float OverdueAssignedReminderHrs { get; set; }
        public bool OverdueAssignedEscallate { get; set; }
        public float OverdueResolvedHrs { get; set; }
        public float OverdueResolvedReminderHrs { get; set; }
        public bool OverdueResolvedEscallate { get; set; }
        public float OverdueResolvedClosedHrs { get; set; }
        public float OverdueResolvedClosedReminderHrs { get; set; }
        public float OverdueClosedEscallate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        
    }
}
