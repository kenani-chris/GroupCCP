using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.ComplaintFollowUps
{
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ComplaintFollowUp> ComplaintFollowUp { get;set; }

        public async Task OnGetAsync()
        {
            ComplaintFollowUp = await _context.ComplaintFollowUp
                .Include(c => c.FollowUps)
                .Include(c => c.Logs)
                .Include(c => c.Staff).ToListAsync();
        }
    }
}
