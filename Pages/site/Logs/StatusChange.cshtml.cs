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
    public class StatusChangeModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public StatusChangeModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }
        public int LogTypeId { get; set; }
        public string PageTitle { get; set; }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public bool StaffHasPerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public IList<string> PageLists { get; set; }
        public IList<string> ErrorList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogType, int? LogId, int? StatusId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null || LogType == null || LogId == null || StatusId == null)
            {
                return NotFound();
            }
            else
            {
                Company = await _context.Company.Include(c => c.Group).FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
                ComplaintLogDetail = await _context.ComplaintLogDetail.FirstOrDefaultAsync(c => c.LogId == LogId);
                if (Company == null)
                {
                    return NotFound("Company not found");
                }
            }

            // Common Functions
            Defaults Default = new(_context);
            TasksFunctions tasks = new(_context);
            ErrorList = new List<string>();

            bool StatusChanged = tasks.StatusChange(ComplaintLogDetail.LogId, (int)StatusId, ErrorList, Company.CompanyId);
            if (StatusChanged == true && ErrorList.Any() == false && ComplaintLogDetail.LogStatusId != 5)
            {
                _context.Attach(ComplaintLogDetail).State = EntityState.Modified;

                try
                {
                    ComplaintLogDetail.LogStatusId = (int)StatusId;
                    _context.Attach(ComplaintLogDetail).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.ComplaintLogDetail.Any(e => e.LogId == LogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToPage("./Details", new { Company.CompanyId, LogType = (int)LogType, LogId = (int)LogId });
            }

            //Initialize Permissions required
            LogTypeId = (int)LogType;

            //Other Context Objects
            PageTitle = Default.GetLogType(LogTypeId) + " Complaints - Status Change";
            StaffHasPerm = true;

            return Page();
        }
    }
}
