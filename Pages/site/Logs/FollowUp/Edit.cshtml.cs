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

namespace GroupCCP.Pages.site.Logs.FollowUp
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComplaintFollowUp ComplaintFollowUp { get; set; }
        public Company Company { get; set; }
        public int LogTypeId { get; set; }
        public string PageTitle { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool LogFollowUpAddPerm { get; set; }
        public bool LogFollowUpDeletePerm { get; set; }
        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogType, int? LogId, int? FollowUpId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogId == null || LogType == null || FollowUpId == null)
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
            LogTypeId = (int)LogType;
            PermissionRequired = "Edit";
            PermissionEntity = Default.GetLogPermissionEntity(LogTypeId, "FollowUp");

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

            PageTitle = Default.GetLogType(LogTypeId) + " Complaints - Edit FollowUp Log " + ComplaintLogDetail.LogId;
            LogFollowUpAddPerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Add");
            LogFollowUpDeletePerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Delete"); 
            ViewData["FollowUpTypeId"] = new SelectList(_context.FollowUpCalls.Where(c => c.CompanyId == CompanyId), "FollowUpId", "FollowUpType");
           
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int CompanyId, int LogType, int LogId, int FollowUpId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ComplaintFollowUp.FollowUpId = FollowUpId;
            ComplaintFollowUp.LogId = LogId;
            ComplaintFollowUp.Staff = await _context.StaffAccount.Where(c => c.CompanyId == CompanyId).FirstOrDefaultAsync(c => c.User.UserName == User.Identity.Name);
            ComplaintFollowUp.FollowUpDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            _context.Attach(ComplaintFollowUp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintFollowUpExists(ComplaintFollowUp.FollowUpId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Company = await _context.Company
                .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
            LogTypeId = (int)LogType;
            ComplaintLogDetail = await _context.ComplaintLogDetail
                .FirstOrDefaultAsync(m => m.LogId == LogId);


            return RedirectToPage("../Details", new { Company.CompanyId, LogType = LogTypeId, ComplaintLogDetail.LogId });

        }

        private bool ComplaintFollowUpExists(int id)
        {
            return _context.ComplaintFollowUp.Any(e => e.FollowUpId == id);
        }
    }
}
