using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.Levels
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
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
        public bool CustomerEditPerm { get; set; }
        public bool CustomerDeletePerm { get; set; }


        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LevelId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LevelId == null)
            {
                return NotFound();
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

                Level = await _context.Level
                    .Include(l => l.LevelCategory)
                    .Include(l => l.ParentLevel).FirstOrDefaultAsync(m => m.LevelId == LevelId);

                if (Company == null || Level == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "Edit";
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
            PageTitle = "Admin - Level Edit";
            ViewData["LevelCategoryId"] = new SelectList(_context.LevelCategory.Where(c => c.CompanyId == CompanyId), "LevelCategoryId", "CategorName");
            ViewData["ParentId"] = new SelectList(_context.Level.Include(c => c.LevelCategory).Where(c => c.LevelCategory.CompanyId == CompanyId), "LevelId", "LevelName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int CompanyId, int LevelId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Level).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelExists(Level.LevelId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { CompanyId, LevelId });
        }

        private bool LevelExists(int id)
        {
            return _context.Level.Any(e => e.LevelId == id);
        }

    }
}