using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.FollowUpTypes
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FollowUpCalls FollowUpCalls { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }


        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? FollowUpTypeId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || FollowUpTypeId == null)
            {
                return NotFound();
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

                FollowUpCalls = await _context.FollowUpCalls
                    .Include(f => f.Company).FirstOrDefaultAsync(m => m.FollowUpId == FollowUpTypeId);

                if (Company == null || FollowUpCalls == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "Delete";
            PermissionEntity = "Admin - FollowUpTypes";

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
            PageTitle = "Admin - FollowUpTypes Delete";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int CompanyId, int FollowUpTypeId)
        {

            FollowUpCalls = await _context.FollowUpCalls.FindAsync(FollowUpTypeId);

            if (FollowUpCalls != null)
            {
                _context.FollowUpCalls.Remove(FollowUpCalls);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index", new { CompanyId });
        }
    }
}