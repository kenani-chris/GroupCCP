using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupCCP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace GroupCCP.Pages.site
{
    public class IndexModel : PageModel
    {
        public readonly GroupCCP.Data.ApplicationDbContext _context;
        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }

        public IActionResult OnGet(int? CompanyId)
        {
            if (CompanyId == null)
            {
                return NotFound();
            }
        }
    }
}
