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

namespace GroupCCP.Pages.site.Logs.CorrectiveInfo
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComplaintCorrectiveInfo ComplaintCorrectiveInfo { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public Company Company { get; set; }
        public int LogTypeId { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool LogCorrectiveAddPerm { get; set; }
        public bool LogCorrectiveDeletePerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogType, int? LogId, int? CorrectiveId)
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

                ComplaintCorrectiveInfo = await _context.ComplaintCorrectiveInfo
                    .Include(c => c.Log)
                    .Include(c => c.StaffAccount).FirstOrDefaultAsync(m => m.CorrectiveId == CorrectiveId);

                if (Company == null || ComplaintLogDetail == null || ComplaintCorrectiveInfo == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            LogTypeId = (int)LogType;
            PermissionRequired = "Edit";
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
            PageTitle = Default.GetLogType(LogTypeId) + " Complaint - Edit Corrective Log " + ComplaintLogDetail.LogId;
            ViewData["CorrectiveComponentId"] = new SelectList(_context.ComplaintProductComponent, "ProductID", "ProductComponent");
            LogCorrectiveAddPerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Add");
            LogCorrectiveDeletePerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Delete");
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

            ComplaintCorrectiveInfo.LogId = LogId;
            ComplaintCorrectiveInfo.StaffAccount = await _context.StaffAccount.Where(c => c.CompanyId == CompanyId).FirstOrDefaultAsync(c => c.User.UserName == User.Identity.Name);
            ComplaintCorrectiveInfo.CorrectiveInfoDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            _context.Attach(ComplaintCorrectiveInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintCorrectiveInfoExists(ComplaintCorrectiveInfo.CorrectiveId))
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

        private bool ComplaintCorrectiveInfoExists(int id)
        {
            return _context.ComplaintCorrectiveInfo.Any(e => e.CorrectiveId == id);
        }
    }
}
