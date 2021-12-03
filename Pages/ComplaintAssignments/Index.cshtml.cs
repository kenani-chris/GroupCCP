using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.ComplaintAssignments
{
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ComplaintAssignment> ComplaintAssignment { get;set; }

        public async Task OnGetAsync()
        {
            ComplaintAssignment = await _context.ComplaintAssignment
                .Include(c => c.Log)
                .Include(c => c.Staff).ToListAsync();
        }
    }
}
