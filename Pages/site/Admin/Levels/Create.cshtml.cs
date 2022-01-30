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

namespace GroupCCP.Pages.site.Admin.Levels
{
    public class CreateModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public CreateModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Level Level { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null)
            {
                return NotFound("Company not found");
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

                if (Company == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "Create";
            PermissionEntity = "Admin - Levels";

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
            PageTitle = "Admin - Levels Create";
            ViewData["LevelCategoryId"] = new SelectList(_context.LevelCategory.Where(c => c.CompanyId == CompanyId), "LevelCategoryId", "CategorName");
            ViewData["ParentId"] = new SelectList(_context.Level.Include(c => c.LevelCategory).Where(c => c.LevelCategory.CompanyId == CompanyId), "LevelId", "LevelName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int CompanyId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Level.Add(Level);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { CompanyId });
        }
    }
}