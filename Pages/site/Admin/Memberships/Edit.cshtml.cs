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

namespace GroupCCP.Pages.site.Admin.Memberships
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LevelMembership LevelMembership { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LevelMembership = await _context.LevelMemberships
                .Include(l => l.Level)
                .Include(l => l.Staff).FirstOrDefaultAsync(m => m.MembershipId == id);

            if (LevelMembership == null)
            {
                return NotFound();
            }
           ViewData["LevelId"] = new SelectList(_context.Level, "LevelId", "LevelName");
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

            _context.Attach(LevelMembership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelMembershipExists(LevelMembership.MembershipId))
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

        private bool LevelMembershipExists(int id)
        {
            return _context.LevelMemberships.Any(e => e.MembershipId == id);
        }
    }
}
