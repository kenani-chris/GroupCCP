using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.Role
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Roles Roles { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool RoleEditPerm { get; set; }
        public bool RoleDeletePerm { get; set; }
        public IList<PermissionAssignment> RolePermissions { get; set; }
        public IList<Permissions> AllPermissions { get; set; }


        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? RoleId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || RoleId == null)
            {
                return NotFound();
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

                Roles = await _context.Roles
                    .Include(r => r.Company).FirstOrDefaultAsync(m => m.RoleId == RoleId);

                if (Company == null || Roles == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "View";
            PermissionEntity = "Admin - Roles";

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
            PageTitle = "Admin - Role Details";
            RoleDeletePerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Delete");
            RoleEditPerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Edit");
            AllPermissions = await _context.Permissions.ToListAsync();

            RolePermissions = await _context.PermissionAssignment
                .Include(c => c.Roles)
                .Include(c => c.Permissions)
                .Where(c => c.RoleId == RoleId)
                .ToListAsync();

            return Page();
        }

    }
}