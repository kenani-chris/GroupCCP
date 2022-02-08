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

namespace GroupCCP.Pages.site.Logs.Vehicle
{
    public class CreateModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public CreateModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ComplaintVehicleInfo ComplaintVehicleInfo { get; set; }
        public Company Company { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public string PageTitle { get; set; }
        public string RedirectTo { get; set; }
        public int LogTypeId { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogType, int? LogId, string Redirect)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogId == null || LogType == null)
            {
                return NotFound();
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

                ComplaintLogDetail = await _context.ComplaintLogDetail
                    .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                    .Include(c => c.Customers)
                    .Include(c => c.Level)
                    .Include(c => c.Means)
                    .Include(c => c.Status).FirstOrDefaultAsync(m => m.LogId == LogId);
                string RedirectTo = (string)Redirect;
                if (RedirectTo == "edit")
                {
                    if (ComplaintLogDetail == null)
                    {
                        return NotFound();
                    }
                }
                if (Company == null)
                {
                    return NotFound();
                }
            }
            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            LogTypeId = (int)LogType;
            PermissionRequired = "Add";
            PermissionEntity = "Vehicle";

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
            PageTitle = "Vehicle - Add";
            ViewData["VehicleBrandId"] = new SelectList(_context.Brands.Where(c => c.CompanyId == CompanyId), "BrandId", "Brand");
            RedirectTo = (string)Redirect;
            return Page();
        }

        [BindProperty]
        public ComplaintCustomerInfo ComplaintCustomerInfo { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int CompanyId, int LogType, int LogId, string Redirect)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ComplaintVehicleInfo.Add(ComplaintVehicleInfo);
            await _context.SaveChangesAsync();

            LogTypeId = (int)LogType;
            if (Redirect == "edit")
            {
                return RedirectToPage("../Edit", new { Company.CompanyId, LogType = LogTypeId, ComplaintLogDetail.LogId });
            }
            else
            {
                return RedirectToPage("../Create", new { CompanyId, LogType = LogTypeId });
            }
        }
    }
}
