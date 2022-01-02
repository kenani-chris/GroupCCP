using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.PermissionAssignments
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public PermissionAssignment PermissionAssignment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PermissionAssignment = await _context.PermissionAssignment
                .Include(p => p.Permissions)
                .Include(p => p.Roles).FirstOrDefaultAsync(m => m.AssignmentId == id);

            if (PermissionAssignment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
