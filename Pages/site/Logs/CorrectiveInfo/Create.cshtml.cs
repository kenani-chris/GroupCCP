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
using Microsoft.AspNetCore.Identity;

namespace GroupCCP.Pages.site.Logs.CorrectiveInfo
{
    public class CreateModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public CreateModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<ComplaintCorrectiveInfo> PreviousCorrectivrInfo { get; set; }
        public Company Company { get; set; }
        public int LogTypeId { get; set; }
        public string PageTitle { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogType, int? LogId)
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
                if (Company == null || ComplaintLogDetail == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            LogTypeId = (int)LogType;
            PermissionRequired = "Add";
            PermissionEntity = Default.GetLogPermissionEntity(LogTypeId, "Corrective");

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
            PageTitle = Default.GetLogType(LogTypeId) + " Complaint - Corrective Log " + ComplaintLogDetail.LogId;
            PreviousCorrectivrInfo = await _context.ComplaintCorrectiveInfo
                .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                .Where(c => c.LogId == LogId).OrderByDescending(c => c.CorrectiveId).ToListAsync();
            
            return Page();
        }

        [BindProperty]
        public ComplaintCorrectiveInfo ComplaintCorrectiveInfo { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int CompanyId, int LogType, int LogId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ComplaintCorrectiveInfo.CorrectiveInfoDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            ComplaintCorrectiveInfo.LogId = LogId;
            
            ComplaintCorrectiveInfo.StaffAccount = await _context.StaffAccount.Where(c => c.CompanyId == CompanyId).FirstOrDefaultAsync(c => c.User.UserName == User.Identity.Name);
            _context.ComplaintCorrectiveInfo.Add(ComplaintCorrectiveInfo);
            await _context.SaveChangesAsync();
            ComplaintLogDetail = await _context.ComplaintLogDetail
                .FirstOrDefaultAsync(m => m.LogId == LogId);
            LogTypeId = (int)LogType;
            Company = await _context.Company
                .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
            return RedirectToPage("../Details", new { Company.CompanyId, LogType = LogTypeId, ComplaintLogDetail.LogId });
        }
    }
}
