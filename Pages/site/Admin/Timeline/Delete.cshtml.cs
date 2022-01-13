using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.Timeline
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Timelines Timelines { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Timelines = await _context.Timelines
                .Include(t => t.Company)
                .Include(t => t.Priority).FirstOrDefaultAsync(m => m.TimeLineId == id);

            if (Timelines == null)
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

            Timelines = await _context.Timelines.FindAsync(id);

            if (Timelines != null)
            {
                _context.Timelines.Remove(Timelines);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
