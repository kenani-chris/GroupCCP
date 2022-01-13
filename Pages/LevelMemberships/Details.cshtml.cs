using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.LevelMemberships
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public LevelMembership LevelMembership { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LevelMembership = await _context.LevelMemberships
                .Include(l => l.Level)
                .Include(l => l.Staff).FirstOrDefaultAsync(m => m.MembershipId == id);

            if (LevelMembership == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
