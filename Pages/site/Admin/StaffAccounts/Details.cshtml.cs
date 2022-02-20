using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.StaffAccounts
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public StaffAccount StaffAccount { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccounts { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool AccountEditPerm { get; set; }
        public bool AccountDeletePerm { get; set; }
        public IList<LevelMembership> Membership { get; set;}
        public IList<RoleAssignment> RoleAssignment { get; set;}


        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? AccountId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || AccountId == null)
            {
                return NotFound();
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);


                StaffAccounts = await _context.StaffAccount
                    .Include(s => s.Company)
                    .Include(s => s.User).FirstOrDefaultAsync(m => m.AccountId == AccountId);

                if (Company == null || StaffAccounts == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "View";
            PermissionEntity = "Admin - StaffAccounts";

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
            PageTitle = "Admin - StaffAccounts Details";
            AccountDeletePerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Delete");
            AccountEditPerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Edit");

            RoleAssignment = await _context.RoleAssignment
                .Include(s => s.Roles)
                .Include(s => s.Staff).ThenInclude(s => s.User)
                .Where(s => s.StaffId == AccountId)
                .ToListAsync();

            Membership = await _context.LevelMemberships
                .Include(l => l.Level)
                .Include(l => l.Staff).ThenInclude(l => l.User)
                .Where(s => s.StaffId == AccountId)
                .ToListAsync();

            return Page();
        }

    }
}