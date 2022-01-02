namespace GroupCCP.Models
{
    public class LevelMembership
    {
        public int MembershipId { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public int StaffId { get; set; }
        public StaffAccount Staff { get; set; }
        public string MembershipRole { get; set; }
        public bool MembershipActive { get; set; }
    }
}
