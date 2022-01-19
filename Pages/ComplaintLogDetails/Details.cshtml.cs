using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.ComplaintLogDetails
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ComplaintLogDetail ComplaintLogDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComplaintLogDetail = await _context.ComplaintLogDetail
                .Include(c => c.Brands)
                .Include(c => c.Customers)
                .Include(c => c.Level)
                .Include(c => c.Means)
                .Include(c => c.Priority)
                .Include(c => c.StaffAccount)
                .Include(c => c.Status).FirstOrDefaultAsync(m => m.LogId == id);

            if (ComplaintLogDetail == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
