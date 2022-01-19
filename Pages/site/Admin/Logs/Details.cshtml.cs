using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.Logs
{
    public class DetailsModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DetailsModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public IList<ComplaintAssignment> ComplaintAssignment { get; set; }
        public IList<ComplaintCorrectiveInfo> ComplaintCorrectiveInfo { get; set; }
        public IList<ComplaintFollowUp> ComplaintFollowUp { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool LogEditPerm { get; set; }
        public bool LogDeletePerm { get; set; }
        public bool LogAssignViewPerm { get; set; }
        public bool LogAssignAddPerm { get; set; }
        public bool LogAssignEditPerm { get; set; }
        public bool LogFollowUpViewPerm { get; set; }
        public bool LogFollowUpAddPerm { get; set; }
        public bool LogFollowUpEditPerm { get; set; }
        public bool LogCorrectiveViewPerm { get; set; }
        public bool LogCorrectiveAddPerm { get; set; }
        public bool LogCorrectiveEditPerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogId == null)
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
                    .Include(c => c.Priority)
                    .Include(c => c.Status).FirstOrDefaultAsync(m => m.LogId == LogId);
                if (Company == null || ComplaintLogDetail == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "View";
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

            //Other Context Objects
            PageTitle = "Admin - Log " + ComplaintLogDetail.LogId;

            ComplaintAssignment = await _context.ComplaintAssignment
                .Include(c => c.Log)
                .Include(c => c.Staff)
                .ThenInclude(c => c.User)
                .Where(c => c.LogId == LogId).OrderByDescending(d => d.AssignmentId).ToListAsync();

            ComplaintCorrectiveInfo = await _context.ComplaintCorrectiveInfo
                .Include(c => c.Log)
                .Include(c => c.StaffAccount)
                .ThenInclude(c => c.User)
                .Where(c => c.LogId == LogId)
                .OrderByDescending(c => c.CorrectiveId)
                .ToListAsync();

            ComplaintLogDetail = await _context.ComplaintLogDetail
                .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                .Include(c => c.Customers)
                .Include(c => c.Level)
                .Include(c => c.Means)
                .Include(c => c.Status).FirstOrDefaultAsync(m => m.LogId == LogId);

            ComplaintFollowUp = await _context.ComplaintFollowUp
                .Include(c => c.FollowUpCalls)
                .Include(c => c.Log)
                .Include(c => c.Staff).ThenInclude(c => c.User)
                .Where(c => c.LogId == LogId)
                .OrderByDescending(c => c.FollowUpId)
                .ToListAsync();

            // Check other permissions
            LogEditPerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Edit");
            LogDeletePerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Delete");

            LogAssignViewPerm = Default.StaffHasPermission(StaffAccount,  "Admin - Assignment", "List");
            LogAssignAddPerm = Default.StaffHasPermission(StaffAccount, "Admin - Assignment", "Create");
            LogAssignEditPerm = Default.StaffHasPermission(StaffAccount, "Admin - Assignment", "Edit");

            LogFollowUpViewPerm = Default.StaffHasPermission(StaffAccount, "Admin - FollowUp", "List");
            LogFollowUpAddPerm = Default.StaffHasPermission(StaffAccount, "Admin - FollowUp", "Create");
            LogFollowUpEditPerm = Default.StaffHasPermission(StaffAccount, "Admin - FollowUp", "Edit");

            LogCorrectiveViewPerm = Default.StaffHasPermission(StaffAccount, "Admin - Corrective", "List");
            LogCorrectiveAddPerm = Default.StaffHasPermission(StaffAccount, "Admin - Corrective", "Create");
            LogCorrectiveEditPerm = Default.StaffHasPermission(StaffAccount, "Admin - Corrective", "Edit");


            return Page();
        }

    }
}
