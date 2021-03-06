using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.FollowUp
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FollowUpCalls FollowUpCalls { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FollowUpCalls = await _context.FollowUpCalls
                .Include(f => f.Company).FirstOrDefaultAsync(m => m.FollowUpId == id);

            if (FollowUpCalls == null)
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

            FollowUpCalls = await _context.FollowUpCalls.FindAsync(id);

            if (FollowUpCalls != null)
            {
                _context.FollowUpCalls.Remove(FollowUpCalls);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
