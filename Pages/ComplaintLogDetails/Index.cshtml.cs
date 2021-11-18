using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.ComplaintLogDetails
{
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ComplaintLogDetail> ComplaintLogDetail { get;set; }

        public async Task OnGetAsync()
        {
            ComplaintLogDetail = await _context.ComplaintLogDetail
                .Include(c => c.Customers)
                .Include(c => c.Level)
                .Include(c => c.Means)
                .Include(c => c.Status).ToListAsync();
        }
    }
}
