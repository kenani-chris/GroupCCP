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
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
