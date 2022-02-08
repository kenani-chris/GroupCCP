using System.Collections.Generic;

namespace GroupCCP.Models
{
    public class ComplaintProductComponent
    {
        public int ProductID { get; set; }
        public string ProductComponent { get; set; }
        public ICollection<ComplaintCorrectiveInfo> ComplaintCorrectiveInfos { get; set; }
    }
}
