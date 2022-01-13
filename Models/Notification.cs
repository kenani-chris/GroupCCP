namespace GroupCCP.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int StaffId { get; set; }
        public StaffAccount Account { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }
    }
}
