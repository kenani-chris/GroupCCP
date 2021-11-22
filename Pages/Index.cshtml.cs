using GroupCCP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IList<StaffAccount> StaffAccount { get; set; }
        public async Task OnGetAsync()
        {
            StaffAccount = await _context.StaffAccount
                .Include(s => s.Company)
                .Where(s => s.Company.CompanyActive.Equals(true))
                .Include(s => s.User)
                .Where(s => s.IsActive.Equals(true))
                .Where(s => s.User.UserName.Equals(User.Identity.Name)).ToListAsync();
        }
    }
}
