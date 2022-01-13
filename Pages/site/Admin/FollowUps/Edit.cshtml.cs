using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.FollowUps
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
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
                .Include(c => c.FollowUpCalls)
                .Include(c => c.Log)
                .Include(c => c.Staff).FirstOrDefaultAsync(m => m.FollowUpId == id);

            if (ComplaintFollowUp == null)
            {
                return NotFound();
            }
           ViewData["FollowUpTypeId"] = new SelectList(_context.FollowUpCalls, "FollowUpId", "FollowUpId");
           ViewData["LogId"] = new SelectList(_context.ComplaintLogDetail, "LogId", "LogId");
           ViewData["StaffId"] = new SelectList(_context.StaffAccount, "AccountId", "UserId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ComplaintFollowUp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintFollowUpExists(ComplaintFollowUp.FollowUpId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ComplaintFollowUpExists(int id)
        {
            return _context.ComplaintFollowUp.Any(e => e.FollowUpId == id);
        }
    }
}
