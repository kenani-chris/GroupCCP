using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GroupCCP.Pages.site.Admin.Memberships
{
    public class CreateModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public CreateModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LevelMembership LevelMembership { get; set; }
        public StaffAccount StaffAccounts { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? AccountId)
        {
            //Check Passed Parameters if are ok
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
            PermissionRequired = "Edit";
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
            PageTitle = "Admin - Tmelines Create";

            ViewData["LevelId"] = new SelectList(_context.Level.Include(c => c.LevelCategory).Where(c => c.LevelCategory.CompanyId == CompanyId), "LevelId", "LevelName");
            ViewData["StaffId"] = _context.StaffAccount.Where(c => c.CompanyId == CompanyId).Select(a => new SelectListItem
            {
                Value = a.AccountId.ToString(),
                Text = a.User.Email
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int CompanyId, int AccountId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            LevelMembership.StaffId = AccountId;
            _context.LevelMemberships.Add(LevelMembership);
            await _context.SaveChangesAsync();

            return RedirectToPage("../StaffAccounts/Index", new { CompanyId, AccountId });
        }
    }
}