using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.Permission
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Permissions Permissions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Permissions = await _context.Permissions.FirstOrDefaultAsync(m => m.PermissionId == id);

            if (Permissions == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
