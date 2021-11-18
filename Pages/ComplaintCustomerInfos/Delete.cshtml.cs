using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.ComplaintCustomerInfos
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
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

            ComplaintCustomerInfo = await _context.ComplaintCustomerInfo.FirstOrDefaultAsync(m => m.CustomerId == id);

            if (ComplaintCustomerInfo == null)
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

            ComplaintCustomerInfo = await _context.ComplaintCustomerInfo.FindAsync(id);

            if (ComplaintCustomerInfo != null)
            {
                _context.ComplaintCustomerInfo.Remove(ComplaintCustomerInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
