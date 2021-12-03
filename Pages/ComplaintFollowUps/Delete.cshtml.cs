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
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComplaintFollowUp = await _context.ComplaintFollowUp.FindAsync(id);

            if (ComplaintFollowUp != null)
            {
                _context.ComplaintFollowUp.Remove(ComplaintFollowUp);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
