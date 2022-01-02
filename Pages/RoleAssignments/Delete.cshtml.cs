using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.RoleAssignments
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoleAssignment = await _context.RoleAssignment.FindAsync(id);

            if (RoleAssignment != null)
            {
                _context.RoleAssignment.Remove(RoleAssignment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
