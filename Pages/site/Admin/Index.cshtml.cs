using GroupCCP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GroupCCP.Pages.site.Admin
{
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null)
            {
                return NotFound("Company not found");
            }
            else
            {
                Company = await _context.Company.Include(c => c.Group).FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
                if (Company == null)
                {
                    return NotFound("Company not found");
                }
            }

            // Common Functions
            Defaults Default = new(_context);
            TasksFunctions tasks = new(_context);

            tasks.GetOverdueAssignedLogs();

            //Initialize Permissions required
            PermissionRequired = "View";
            PermissionEntity = "Admin - Home";

            //Check if Staff has a valid staff account
            if (!Default.UserIsStaff(User.Identity.Name, Company.CompanyId))
            {
                return RedirectToPage("./Errors/NoActiveStaffAccount", new { Company.CompanyId });
            }
            else
            {
                StaffAccount = Default.GetStaffAccount(User.Identity.Name, Company.CompanyId);
                // Check if Staff role has required permissions
                StaffHasPerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, PermissionRequired);

            }

            //Other Context Objects
            PageTitle = "Admin Dashboard";

            return Page();
        }
    }
}
