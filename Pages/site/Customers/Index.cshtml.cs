using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Customers
{
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Company Company { get; set; }
        public IList<ComplaintCustomerInfo> ComplaintCustomerInfo { get;set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId)
        {
            ComplaintCustomerInfo = await _context.ComplaintCustomerInfo
                .Include(c => c.Company).ToListAsync();
            if (CompanyId == null)
            {
                return NotFound();
            }
            Company = await _context.Company
                .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
            return Page();
        }
    }
}
