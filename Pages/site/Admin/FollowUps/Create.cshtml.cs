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

namespace GroupCCP.Pages.site.Admin.FollowUps
{
    public class CreateModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public CreateModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }

        [BindProperty]
        public ComplaintFollowUp ComplaintFollowUp { get; set; }
        public IList<ComplaintFollowUp> PreviousFollowUps { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogId == null)
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
            PermissionRequired = "Create";
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

            PageTitle = "Admin - FollowUp Log " + ComplaintLogDetail.LogId;
            ViewData["FollowUpTypeId"] = new SelectList(_context.FollowUpCalls
                .Where(c => c.CompanyId == CompanyId), "FollowUpId", "FollowUpType");
            PreviousFollowUps = await _context.ComplaintFollowUp
                .Include(c => c.Staff).ThenInclude(c => c.User)
                .Include(c => c.FollowUpCalls)
                .Where(c => c.LogId == LogId).OrderByDescending(c => c.FollowUpId).ToListAsync();

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int CompanyId, int LogId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ComplaintFollowUp.FollowUpDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            ComplaintFollowUp.LogId = LogId;
            ComplaintFollowUp.Staff = await _context.StaffAccount.Where(c => c.CompanyId == CompanyId).FirstOrDefaultAsync(c => c.User.UserName == User.Identity.Name);

            _context.ComplaintFollowUp.Add(ComplaintFollowUp);
            await _context.SaveChangesAsync();
            ComplaintLogDetail = await _context.ComplaintLogDetail
                .FirstOrDefaultAsync(m => m.LogId == LogId);
            Company = await _context.Company
                .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

            return RedirectToPage("../Logs/Details", new { Company.CompanyId, ComplaintLogDetail.LogId });
        }
    }
}
