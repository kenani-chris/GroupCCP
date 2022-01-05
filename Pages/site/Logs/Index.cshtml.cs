using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Logs
{
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }
        public int LogTypeId { get; set; }
        public string PageTitle { get; set; }
        public IList<ComplaintLogDetail> ComplaintLogDetail { get;set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public IList<string> PageLists { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogType)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogType == null)
            {
                return NotFound("Company not found");
            }
            else
            {
                Company = await _context.Company.Include(c => c.Group).FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
                if (Company == null)
                {
                    return NotFound("Company not found");
                }
            }

            // Common Functions
            Defaults Default = new(_context);
            WindowsService windowsService = new(_context);

            //Initialize Permissions required
            LogTypeId = (int)LogType;
            PermissionRequired = "List";
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

            //Other Context Objects
            PageTitle = Default.GetLogType(LogTypeId) + " Complaints - List";
            ComplaintLogDetail = (IList<ComplaintLogDetail>)Default.GetLog(LogTypeId, StaffAccount.AccountId).OrderByDescending(c => c.LogId).ToList();
    
            return Page();
        }
    }
}
