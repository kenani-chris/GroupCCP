using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class Brands
    {
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<ComplaintLogDetail> ComplaintLogDetails { get; set; }
    }
}
