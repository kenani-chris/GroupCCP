using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.Companies
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Company Companies { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool CompanyEditPerm { get; set; }
        public bool CompanyDeletePerm { get; set; }


        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? SelectCompanyId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || SelectCompanyId == null)
            {
                return NotFound();
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);


                Companies = await _context.Company
                    .Include(c => c.Group).FirstOrDefaultAsync(m => m.CompanyId == SelectCompanyId);

                if (Company == null || Companies == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "View";
            PermissionEntity = "Admin - Companies";

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
            PageTitle = "Admin - Companies Details";
            CompanyDeletePerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Delete");
            CompanyEditPerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Edit");

            return Page();
        }

    }
}