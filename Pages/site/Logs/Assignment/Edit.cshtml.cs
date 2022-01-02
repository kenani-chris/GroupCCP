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

namespace GroupCCP.Pages.site.Logs.Assignment
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComplaintAssignment ComplaintAssignment { get; set; }
        public IList<ComplaintAssignment> PreviousAssignment { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public Company Company { get; set; }
        public int LogTypeId { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool LogAssignAddPerm { get; set; }
        public bool LogAssignDeletePerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogType, int? LogId, int? AssignmentId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogId == null || LogType == null || AssignmentId == null)
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

                ComplaintAssignment = await _context.ComplaintAssignment
                .Include(c => c.Log)
                .Include(c => c.Staff).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.AssignmentId == AssignmentId);

                if (Company == null || ComplaintLogDetail == null || ComplaintAssignment == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            LogTypeId = (int)LogType;
            PermissionRequired = "Edit";
            PermissionEntity = Default.GetLogPermissionEntity(LogTypeId, "Assignment");

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

            PageTitle = Default.GetLogType(LogTypeId) + " Complaints - Edit Assign Log " + ComplaintLogDetail.LogId;
            LogAssignAddPerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Add");
            LogAssignDeletePerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, "Delete");
            PreviousAssignment = await _context.ComplaintAssignment
                .Include(c => c.Log)
                .Include(c => c.Staff)
                .ThenInclude(c => c.User)
                .Where(c => c.LogId == LogId)
                .OrderByDescending(c => c.AssignmentId)
                .ToListAsync();
            
            ViewData["StaffAssigned"] = _context.StaffAccount.Where(c => c.CompanyId == CompanyId).Select(a => new SelectListItem
            {
                Value = a.AccountId.ToString(),
                Text = a.User.Email
            }).ToList();

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

            _context.Attach(ComplaintAssignment).State = EntityState.Modified;
            ComplaintAssignment.AssignmentDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            ComplaintAssignment.AssignmentType = "Assignment";
            ComplaintAssignment.LogId = LogId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintAssignmentExists(ComplaintAssignment.AssignmentId))
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

        private bool ComplaintAssignmentExists(int id)
        {
            return _context.ComplaintAssignment.Any(e => e.AssignmentId == id);
        }
    }
}
