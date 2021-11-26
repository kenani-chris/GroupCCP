using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site
{
    public class IndexModel : PageModel
    {
        public readonly GroupCCP.Data.ApplicationDbContext _context;
        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId)
        {
            if (CompanyId == null)
            {
                return NotFound();
            }
            Company = await _context.Company
                .Include(c => c.Group).FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
            return Page();
        }
    }
}
