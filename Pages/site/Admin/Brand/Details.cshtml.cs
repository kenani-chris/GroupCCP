using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.Brand
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ComplaintCustomerInfo ComplaintCustomerInfo { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool BrandEditPerm { get; set; }
        public bool BrandDeletePerm { get; set; }
        public Brands Brand { get; set; }


        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? BrandId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || BrandId == null)
            {
                return NotFound();
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

                Brand = await _context.Brands
                    .Include(b => b.Company).FirstOrDefaultAsync(m => m.BrandId == BrandId);

                if (Company == null || Brand == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "View";
            PermissionEntity = "Admin - Brands";

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
            PageTitle = "Admin - Brand Details";
            BrandDeletePerm = Default.StaffHasPermission(StaffAccount, "Admin - Brands", "Delete");
            BrandEditPerm = Default.StaffHasPermission(StaffAccount, "Admin - Brands", "Edit");

            return Page();
        }

    }
}
