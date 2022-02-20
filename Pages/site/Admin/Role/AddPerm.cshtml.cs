using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GroupCCP.Data;
using GroupCCP.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupCCP.Pages.site.Admin.Role
{
    public class AddPermModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public AddPermModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Permissions Permission { get; set; }
        public Roles Roles { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool CustomerEditPerm { get; set; }
        public bool CustomerDeletePerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? RoleId, int? PermissionId)
        {

            //Check Passed Parameters if are ok
            if (CompanyId == null || RoleId == null || PermissionId == null)
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

                Permission = await _context.Permissions
                    .FirstOrDefaultAsync(c => c.PermissionId == PermissionId);

                if (Company == null || Roles == null || Permission == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "Edit";
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
            PermissionAssignment permissionAssignment = new PermissionAssignment();
            permissionAssignment.RoleId = (int)RoleId;
            permissionAssignment.PermissionId = (int)PermissionId;

            _context.PermissionAssignment.Add(permissionAssignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { CompanyId, RoleId });
        }
    }
}
