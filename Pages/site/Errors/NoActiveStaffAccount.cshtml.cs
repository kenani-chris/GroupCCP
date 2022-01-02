using GroupCCP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GroupCCP.Pages.site.Errors
{
    public class NoActiveStaffAccountModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public NoActiveStaffAccountModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Company Company { get; set; }
        public async Task<IActionResult> OnGetAsync(int? CompanyId)
        {
            if(CompanyId == null)
            {
                return NotFound();
            }
            Company = await _context.Company
                .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

            var CId = (int)CompanyId;
            Defaults Default = new(_context);
            var ValidStaff = Default.UserIsStaff(User.Identity.Name, CId);
            if (!ValidStaff)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("../Index", new { CompanyId = Company.CompanyId });
            }
            
        }
    }
}
