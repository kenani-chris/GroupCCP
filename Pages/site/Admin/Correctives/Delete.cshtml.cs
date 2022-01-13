using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.Correctives
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComplaintCorrectiveInfo ComplaintCorrectiveInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComplaintCorrectiveInfo = await _context.ComplaintCorrectiveInfo
                .Include(c => c.Log)
                .Include(c => c.StaffAccount).FirstOrDefaultAsync(m => m.CorrectiveId == id);

            if (ComplaintCorrectiveInfo == null)
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

            ComplaintCorrectiveInfo = await _context.ComplaintCorrectiveInfo.FindAsync(id);

            if (ComplaintCorrectiveInfo != null)
            {
                _context.ComplaintCorrectiveInfo.Remove(ComplaintCorrectiveInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
