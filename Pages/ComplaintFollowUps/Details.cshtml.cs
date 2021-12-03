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
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ComplaintFollowUp ComplaintFollowUp { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComplaintFollowUp = await _context.ComplaintFollowUp
                .Include(c => c.FollowUps)
                .Include(c => c.Logs)
                .Include(c => c.Staff).FirstOrDefaultAsync(m => m.FollowUpId == id);

            if (ComplaintFollowUp == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
