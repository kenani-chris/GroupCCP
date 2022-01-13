using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.LevelCategories
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
