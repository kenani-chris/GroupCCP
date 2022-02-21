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
using System.Globalization;

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
        public IList<IList<string>> LevelLogs { get; set; }
        public IList<string> LevelLabels { get; set; }
        public IList<string> LevelData { get; set; }


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
                return RedirectToPage("./Errors/NoActiveStaffAccount", new { Company.CompanyId });
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

            for (int i = 0; i < 8; i++)
            {
                var TheDate = DateTime.Now - TimeSpan.FromDays(i);
                var DayStart = TheDate.Date.Add(new TimeSpan(0, 0, 0));
                var DayEnd = TheDate.Date.Add(new TimeSpan(23, 59, 59));
                CultureInfo provider = CultureInfo.InvariantCulture;
                var TheLogs = ThisLogs
                    .Where(c => DateTime.ParseExact(c.StatusSubmitDate, "dd/MM/yyyy hh:mm tt", provider) >= DayStart)
                    .Where(c => DateTime.ParseExact(c.StatusSubmitDate, "dd/MM/yyyy hh:mm tt", provider) <= DayEnd);

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

            LevelLogs = new List<IList<string>>();
            IList<Level> LevelsDown = new List<Level>();
            if(StaffAccount.IsSuperUser == true)
            {
                LevelsDown = await _context.Level
                    .Include(c => c.LevelCategory)
                    .Where(c => c.LevelCategory.CompanyId == CompanyId)
                    .ToListAsync();
            }
            else
            {
                LevelsDown = Default.GetLevelDown(StaffAccount.AccountId);
            }
            LevelLabels = new List<string>();
            LevelData = new List<string>();
            foreach (var Level in LevelsDown)
            {
                IList<string> LevelDetails = new List<string>();
                LevelDetails.Add(Level.LevelName);

                var TotalLogs = await _context.ComplaintLogDetail
                    .Where(c => c.Level == Level)
                    .CountAsync();
                LevelDetails.Add(TotalLogs.ToString());
                var OpenLogs = await _context.ComplaintLogDetail
                    .Where(c => c.Level == Level)
                    .Where(c => c.LogStatusId == 1)
                    .CountAsync();
                LevelDetails.Add(OpenLogs.ToString());
                var AssignedLogs = await _context.ComplaintLogDetail
                    .Where(c => c.Level == Level)
                    .Where(c => c.LogStatusId == 2)
                    .CountAsync();
                LevelDetails.Add(AssignedLogs.ToString());
                var WIPLogs = await _context.ComplaintLogDetail
                    .Where(c => c.Level == Level)
                    .Where(c => c.LogStatusId == 3)
                    .CountAsync();
                LevelDetails.Add(WIPLogs.ToString());
                var ResolvedLogs = await _context.ComplaintLogDetail
                    .Where(c => c.Level == Level)
                    .Where(c => c.LogStatusId == 4)
                    .CountAsync();
                LevelDetails.Add(ResolvedLogs.ToString());
                var ClosedLogs = await _context.ComplaintLogDetail
                    .Where(c => c.Level == Level)
                    .Where(c => c.LogStatusId == 5)
                    .CountAsync();
                LevelDetails.Add(ClosedLogs.ToString());

                LevelLabels.Add(Level.LevelName);
                LevelData.Add(TotalLogs.ToString());
                LevelLogs.Add(LevelDetails);
            }
            
            return Page();
        }
    }
}
