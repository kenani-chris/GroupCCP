﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.Role
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Roles Roles { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool CustomerEditPerm { get; set; }
        public bool CustomerDeletePerm { get; set; }


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
            PageTitle = "Admin - Roles Edit";

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int CompanyId, int RoleId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Roles.CompanyId = CompanyId;
            _context.Attach(Roles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(Roles.RoleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { CompanyId, RoleId });
        }

        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }

    }
}
