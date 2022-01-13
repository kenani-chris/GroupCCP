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

namespace GroupCCP.Pages.site.Admin.LevelCategories
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LevelCategory LevelCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LevelCategory = await _context.LevelCategory
                .Include(l => l.Company)
                .Include(l => l.ParentCategory).FirstOrDefaultAsync(m => m.LevelCategoryId == id);

            if (LevelCategory == null)
            {
                return NotFound();
            }
           ViewData["CompanyId"] = new SelectList(_context.Company, "CompanyId", "CompanyName");
           ViewData["ParentId"] = new SelectList(_context.LevelCategory, "LevelCategoryId", "CategorName");
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

            _context.Attach(LevelCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelCategoryExists(LevelCategory.LevelCategoryId))
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

        private bool LevelCategoryExists(int id)
        {
            return _context.LevelCategory.Any(e => e.LevelCategoryId == id);
        }
    }
}
