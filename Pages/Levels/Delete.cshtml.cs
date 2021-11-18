using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.Levels
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Level Level { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Level = await _context.Level
                .Include(l => l.LevelCategory)
                .Include(l => l.ParentLevel).FirstOrDefaultAsync(m => m.LevelId == id);

            if (Level == null)
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

            Level = await _context.Level.FindAsync(id);

            if (Level != null)
            {
                _context.Level.Remove(Level);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
