using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Logs.Responsibility
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public ComplaintResponsibility ComplaintResponsibility { get; set; }
        public Company Company { get; set; }
        public int LogTypeId { get; set; }
        public string PageTitle { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogType, int? LogId, int? ResponsibilityId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogId == null || LogType == null)
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

                ComplaintResponsibility = await _context.ComplaintResponsibility
                    .Include(c => c.Log).FirstOrDefaultAsync(m => m.ResponsibilityId == ResponsibilityId);

                if (Company == null || ComplaintLogDetail == null || ComplaintResponsibility == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            LogTypeId = (int)LogType;
            PermissionRequired = "Delete";
            PermissionEntity = Default.GetLogPermissionEntity(LogTypeId, "Responsibility");

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
            PageTitle = Default.GetLogType(LogTypeId) + " Complaint - Delete Corrective Log " + ComplaintLogDetail.LogId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int CompanyId, int LogType, int LogId, int ResponsibilityId)
        {

            ComplaintResponsibility = await _context.ComplaintResponsibility.FindAsync(ResponsibilityId);

            if (ComplaintResponsibility != null)
            {
                _context.ComplaintResponsibility.Remove(ComplaintResponsibility);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Details", new { CompanyId, LogType, LogId });
        }
    }
}