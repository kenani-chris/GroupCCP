using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.RoleAssignments
{
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RoleAssignment> RoleAssignment { get;set; }

        public async Task OnGetAsync()
        {
            RoleAssignment = await _context.RoleAssignment
                .Include(r => r.Roles)
                .Include(r => r.Staff).ToListAsync();
        }
    }
}
