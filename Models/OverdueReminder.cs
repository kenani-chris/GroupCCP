namespace GroupCCP.Models
{
    public class OverdueReminder
    {
        public int ReminderId { get; set; }
        public int LogId { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public int StaffId { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string Reminder { get; set; }
        public string MessageType { get; set; }
        public bool IsSent { get; set; }
    }
}
