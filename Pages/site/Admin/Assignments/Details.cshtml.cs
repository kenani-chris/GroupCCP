using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.Assignments
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ComplaintAssignment ComplaintAssignment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComplaintAssignment = await _context.ComplaintAssignment
                .Include(c => c.Log)
                .Include(c => c.Staff).FirstOrDefaultAsync(m => m.AssignmentId == id);

            if (ComplaintAssignment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
