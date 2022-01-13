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

namespace GroupCCP.Pages.site.Admin.RolesAssignments
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RoleAssignment RoleAssignment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoleAssignment = await _context.RoleAssignment
                .Include(r => r.Roles)
                .Include(r => r.Staff).FirstOrDefaultAsync(m => m.RoleAssignmentId == id);

            if (RoleAssignment == null)
            {
                return NotFound();
            }
           ViewData["RoleID"] = new SelectList(_context.Roles, "RoleId", "RoleId");
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

            _context.Attach(RoleAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleAssignmentExists(RoleAssignment.RoleAssignmentId))
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

        private bool RoleAssignmentExists(int id)
        {
            return _context.RoleAssignment.Any(e => e.RoleAssignmentId == id);
        }
    }
}
