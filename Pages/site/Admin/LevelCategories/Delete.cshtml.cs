using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.LevelCategories
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public LevelCategory LevelCategory { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }


        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LevelCategoryId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LevelCategoryId == null)
            {
                return NotFound();
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

                LevelCategory = await _context.LevelCategory
                   .Include(l => l.Company)
                   .Include(l => l.ParentCategory).FirstOrDefaultAsync(m => m.LevelCategoryId == LevelCategoryId);

                if (Company == null || LevelCategory == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "Delete";
            PermissionEntity = "Admin - LevelCategories";

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
            PageTitle = "Admin - LevelCats. Delete";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int CompanyId, int LevelCategoryId)
        {

            LevelCategory = await _context.LevelCategory.FindAsync(LevelCategoryId);

            if (LevelCategory != null)
            {
                _context.LevelCategory.Remove(LevelCategory);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index", new { CompanyId });
        }
    }
}