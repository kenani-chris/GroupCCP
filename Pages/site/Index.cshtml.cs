using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

using System.Collections;

namespace GroupCCP.Pages.site
{
    public class IndexModel : PageModel
    {
        public readonly GroupCCP.Data.ApplicationDbContext _context;
        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Company Company { get; set; }
        public IList<ComplaintAssignment> MyUnAttendedLogs { get; set; }
        public IList<ComplaintAssignment> MyWIPLogs { get; set; }
        public IList<ComplaintAssignment> MyLogs { get; set; }
        public IList<ComplaintAssignment> MyAssignedLogs { get; set; }
        public IList<ComplaintAssignment> MyResolvedLogs { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired {get; set;}
        public string PermissionEntity {get; set;}
        public bool StaffHasPerm { get; set; }
        public IList<string> PageLists { get; set; }
        public IList<int> WeekResults { get; set; }
        public async Task<IActionResult> OnGetAsync(int? CompanyId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null)
            {
                return NotFound("Company not found");
            }
            else
            {
                Company = await _context.Company.Include(c => c.Group).FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
                if(Company == null)
                {
                    return NotFound("Company not found");
                }
            }

            TasksFunctions tasks = new(_context);
            tasks.AssignLogs();
            
            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = null;
            PermissionEntity = null;

            //Check if Staff has a valid staff account
            if (!Default.UserIsStaff(User.Identity.Name, Company.CompanyId))
            {
                return RedirectToPage("./Errors/NoActiveStaffAccount", new { CompanyId = Company.CompanyId });
            }
            else
            {
                StaffAccount = Default.GetStaffAccount(User.Identity.Name, Company.CompanyId);
                // Check if Staff role has required permissions
                StaffHasPerm = Default.StaffHasPermission(StaffAccount, PermissionEntity, PermissionRequired);
            }
            
            MyLogs = await _context.ComplaintAssignment.Where(c => c.Staff == StaffAccount).ToListAsync();
            WeekResults = new List<int>();
            var ThisLogs = await _context.ComplaintLogDetail.Where(c => c.StaffAccount == StaffAccount).ToListAsync();

            for (int i = 1; i < 8; i++)
            {
                var TheDate = DateTime.Now - TimeSpan.FromDays(i);
                var DayStart = TheDate.Date.Add(new TimeSpan(0, 0, 0));
                var DayEnd = TheDate.Date.Add(new TimeSpan(23, 59, 59));

                var TheLogs = ThisLogs
                    .Where(c => DateTime.Parse(c.StatusSubmitDate) >= DayStart)
                    .Where(c => DateTime.Parse(c.StatusSubmitDate) <= DayEnd);

                Console.WriteLine(TheDate.ToString() + " - " + i.ToString() + " - " + TheLogs.Count().ToString());
                WeekResults.Add(TheLogs.Count());
            }
            
            MyAssignedLogs = await _context.ComplaintAssignment.Include(c => c.Log).ThenInclude(c => c.Status).Where(c => c.Staff == StaffAccount).ToListAsync();
            
            foreach (var item in MyLogs)
            {
                var otherAssignment = _context.ComplaintAssignment.Where(c => c.LogId == item.LogId).OrderByDescending(c => c.AssignmentId).First();
                if (otherAssignment != null)
                {
                    if (otherAssignment.AssignmentId > item.AssignmentId)
                    {
                        MyAssignedLogs.Remove(item);
                    }
                }
            }
            MyUnAttendedLogs = new List<ComplaintAssignment>();
            MyWIPLogs = new List<ComplaintAssignment>();
            MyResolvedLogs = new List<ComplaintAssignment>();
            foreach (var item in MyAssignedLogs)
            {
                if (item.Log.Status.Status == "Assigned")
                {
                    MyUnAttendedLogs.Add(item);
                }
                if (item.Log.Status.Status == "WIP")
                {
                    MyWIPLogs.Add(item);
                }
                if (item.Log.Status.Status == "Resolved")
                {
                    MyResolvedLogs.Add(item);
                }
            }
            
            return Page();
        }
    }
}
