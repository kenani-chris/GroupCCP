using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GroupCCP.Data;
using GroupCCP.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupCCP.Pages.site.Customers
{
    public class CreateModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public CreateModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogTypeId)
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "CompanyName");
            if (CompanyId == null)
            {
                return NotFound();
            }
            Company = await _context.Company
                .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
            
            PageTitle = "Customer - Create";
            return Page();
        }

        [BindProperty]
        public ComplaintCustomerInfo ComplaintCustomerInfo { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ComplaintCustomerInfo.Add(ComplaintCustomerInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
