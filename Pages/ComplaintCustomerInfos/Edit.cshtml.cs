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

namespace GroupCCP.Pages.ComplaintCustomerInfos
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComplaintCustomerInfo ComplaintCustomerInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComplaintCustomerInfo = await _context.ComplaintCustomerInfo
                .Include(c => c.Company).FirstOrDefaultAsync(m => m.CustomerId == id);

            if (ComplaintCustomerInfo == null)
            {
                return NotFound();
            }
           ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "CompanyName");
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

            _context.Attach(ComplaintCustomerInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintCustomerInfoExists(ComplaintCustomerInfo.CustomerId))
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

        private bool ComplaintCustomerInfoExists(int id)
        {
            return _context.ComplaintCustomerInfo.Any(e => e.CustomerId == id);
        }
    }
}
