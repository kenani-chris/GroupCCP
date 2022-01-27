using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Admin.FollowUps
{
    public class DeleteModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public DeleteModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComplaintFollowUp ComplaintFollowUp { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogId, int? FollowUpId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogId == null || FollowUpId == null)
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
                ComplaintFollowUp = await _context.ComplaintFollowUp
                    .Include(c => c.FollowUpCalls)
                    .Include(c => c.Log)
                    .Include(c => c.Staff).FirstOrDefaultAsync(m => m.FollowUpId == FollowUpId);
                if (Company == null || ComplaintLogDetail == null || ComplaintFollowUp == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "Delete";
            PermissionEntity = "Admin - FollowUp";

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
            PageTitle = "Admin - Delete FollowUp Log " + ComplaintLogDetail.LogId;
            Console.WriteLine("This is sth worth saying don't you think");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int CompanyId, int LogId, int FollowUpId)
        {

            ComplaintFollowUp = await _context.ComplaintFollowUp.FindAsync(FollowUpId);

            if (ComplaintFollowUp != null)
            {
                _context.ComplaintFollowUp.Remove(ComplaintFollowUp);
                await _context.SaveChangesAsync();
            }

            ComplaintLogDetail = await _context.ComplaintLogDetail
                .FirstOrDefaultAsync(m => m.LogId == LogId);
            Company = await _context.Company
                .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

            return RedirectToPage("../Logs/Details", new { Company.CompanyId, ComplaintLogDetail.LogId });
        }
    }
}
