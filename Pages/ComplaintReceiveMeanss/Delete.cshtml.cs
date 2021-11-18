using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.ComplaintReceiveMeanss
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComplaintReceiveMeans ComplaintReceiveMeans { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComplaintReceiveMeans = await _context.ComplaintReceiveMeans.FirstOrDefaultAsync(m => m.MeansId == id);

            if (ComplaintReceiveMeans == null)
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

            ComplaintReceiveMeans = await _context.ComplaintReceiveMeans.FindAsync(id);

            if (ComplaintReceiveMeans != null)
            {
                _context.ComplaintReceiveMeans.Remove(ComplaintReceiveMeans);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
