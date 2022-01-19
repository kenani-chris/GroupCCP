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

namespace GroupCCP.Pages.site.Admin.Logs
{
    public class CreateModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public CreateModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }
        public ComplaintLogStatus Status { get; set; }
        public int LogTypeId { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public string CurrentDateTime { get; set; }
        public async Task<IActionResult> OnGetAsync(int? CompanyId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null)
            {
                return NotFound();
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
            PermissionEntity = "Admin - Logs";

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

            // Other Context Objects
            PageTitle = "Admin - Logs - Add";

            ViewData["LogCustomerId"] = new SelectList(_context.ComplaintCustomerInfo
                .Where(c => c.Company == Company), "CustomerId", "Customer");
            ViewData["LogLevelId"] = new SelectList(_context.Level
                .Include(c => c.LevelCategory)
                .ThenInclude(c => c.Company)
                .Where(c => c.LevelCategory.Company == Company), "LevelId", "LevelName");
            ViewData["LogMeansId"] = new SelectList(_context.ComplaintReceiveMeans, "MeansId", "Means");
            ViewData["PriorityId"] = new SelectList(_context.Priority, "PriorityId", "PriorityName");
            ViewData["BrandId"] = new SelectList(_context.Brands
                .Where(x => x.Company == Company), "BrandId", "Brand");

            return Page();
        }

        [BindProperty]
        public ComplaintLogDetail ComplaintLogDetail { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int CompanyId)
        {
            Defaults Default = new(_context);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Status = await _context.ComplaintLogStatus
                .FirstOrDefaultAsync(s => s.Status == "Open");
            Company = await _context.Company
                .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

            ComplaintLogDetail.StatusSubmitDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            ComplaintLogDetail.LogStatusId = Status.StatusId;
            ComplaintLogDetail.StaffId = Default.GetStaffAccount(User.Identity.Name, Company.CompanyId).AccountId;
            _context.ComplaintLogDetail.Add(ComplaintLogDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { Company.CompanyId });
        }
    }
}
