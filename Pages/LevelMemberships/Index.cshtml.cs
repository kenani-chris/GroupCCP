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
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<LevelMembership> LevelMembership { get;set; }

        public async Task OnGetAsync()
        {
            LevelMembership = await _context.LevelMemberships
                .Include(l => l.Level)
                .Include(l => l.Staff).ToListAsync();
        }
    }
}
