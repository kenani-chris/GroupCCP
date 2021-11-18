using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.StaffAccounts
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public StaffAccount StaffAccount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StaffAccount = await _context.StaffAccount
                .Include(s => s.ApplicationUser)
                .Include(s => s.Company).FirstOrDefaultAsync(m => m.AccountId == id);

            if (StaffAccount == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
