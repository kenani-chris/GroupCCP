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

namespace GroupCCP.Pages.site.Logs
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public Company Company { get; set; }
        public int LogTypeId { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }


        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogType, int? LogId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogType == null || LogId == null)
            {
                return NotFound("Company not found");
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
                if (Company == null || ComplaintLogDetail == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            LogTypeId = (int)LogType;
            PermissionRequired = "Edit";
            PermissionEntity = Default.GetLogPermissionEntity(LogTypeId, "Complaint");

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
            PageTitle = Default.GetLogType(LogTypeId) + " Complaints - Log " + ComplaintLogDetail.LogId + " - Edit";

            ViewData["LogCustomerId"] = new SelectList(_context.ComplaintCustomerInfo
                .Where(c => c.Company == Company), "CustomerId", "Customer");
            ViewData["LogLevelId"] = new SelectList(_context.Level
                .Include(c => c.LevelCategory)
                .ThenInclude(c => c.Company)
                .Where(c => c.LevelCategory.Company == Company), "LevelId", "LevelName");
            ViewData["LogMeansId"] = new SelectList(_context.ComplaintReceiveMeans, "MeansId", "Means");
            ViewData["BrandId"] = new SelectList(_context.Brands
                .Where(x => x.Company == Company), "BrandId", "Brand");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int CompanyId, int LogType, int LogId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ComplaintLogDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintLogDetailExists(ComplaintLogDetail.LogId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            LogTypeId = (int)LogType;
            Company = await _context.Company
               .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
            ComplaintLogDetail = await _context.ComplaintLogDetail
               .Include(c => c.Status).FirstOrDefaultAsync(m => m.LogId == LogId);
            return RedirectToPage("./Details", new { Company.CompanyId, LogType = LogTypeId, ComplaintLogDetail.LogId });
        }

        private bool ComplaintLogDetailExists(int id)
        {
            return _context.ComplaintLogDetail.Any(e => e.LogId == id);
        }
    }
}
