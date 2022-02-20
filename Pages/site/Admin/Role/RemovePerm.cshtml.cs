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
    public class RemovePermModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public RemovePermModel(GroupCCP.Data.ApplicationDbContext context)
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
        [BindProperty]
        public PermissionAssignment PermissionAssignment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? RoleId, int? AssignmentId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || RoleId == null || AssignmentId == null)
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


                PermissionAssignment = await _context.PermissionAssignment
                    .Include(p => p.Permissions)
                    .Include(p => p.Roles).FirstOrDefaultAsync(m => m.AssignmentId == AssignmentId);


                if (Company == null || Roles == null || PermissionAssignment == null)
                {
                    return NotFound();
                }
            }
            Console.WriteLine("this is not fair");
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

            if(StaffHasPerm == true)
            {
                _context.PermissionAssignment.Remove(PermissionAssignment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Details", new { CompanyId, RoleId });
        }
    }
}
