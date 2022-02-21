using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.site.Report
{
    public class IndexModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public IndexModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<ComplaintLogDetail> ComplaintLogs { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public bool RoleCreatePerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public IList<IList<string>> Records { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId)
        {
            //Check Passed Parameters if are ok
            if (CompanyId == null)
            {
                return NotFound("Company not found");
            }
            else
            {
                Company = await _context.Company
                    .Include(c => c.Group)
                    .FirstOrDefaultAsync(c => c.CompanyId == CompanyId);

                if (Company == null)
                {
                    return NotFound();
                }
            }

            // Common Functions
            Defaults Default = new(_context);

            //Initialize Permissions required
            PermissionRequired = "List";
            PermissionEntity = "Report";

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
            Records = new List<IList<string>>();
            PageTitle = "Report - Logs";
            ComplaintLogs = new List<ComplaintLogDetail>();
            if (StaffAccount.IsSuperUser == false)
            {
                ComplaintLogs = Default.GetLevelAndLevelDownLogs(StaffAccount.AccountId);
            }
            else
            {
                ComplaintLogs = await _context.ComplaintLogDetail
                    .Include(c => c.ComplaintVehicleInfo)
                    .Include(c => c.Status)
                    .Include(c => c.Means)
                    .Include(c => c.Level)
                    .Include(c => c.Customers)
                    .Include(c => c.Priority)
                    .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                    .ToListAsync();
            }

            foreach(var log in ComplaintLogs)
            {
                    
                var DateReceived = log.StatusSubmitDate;
                var ReceivedMeans = log.Means.Means;
                var Level = log.Level.LevelName;
                var PIC = Default.GetStaffAssignedLog(log.LogId) == null ? null : Default.GetStaffAssignedLog(log.LogId).User.Email;
                var CRSupportMember = "";
                var Registration = log.ComplaintVehicleInfo.VehicleRegistrationNumber;
                var Model = log.ComplaintVehicleInfo.VehicleModel;
                var Customer = log.Customers.CustomerName;
                var CustomerCell = log.Customers.CustomerCell;
                var Complaint = log.CustomerComplaint;
                var Request = log.CustomerRequest;
                var RootCause = Default.GetCorrectives(log.LogId)[0];
                var CorrectiveAction = Default.GetCorrectives(log.LogId)[1];
                var Status = log.Status.Status;
                var StatusDate = "";
                var ProgressUpdate = Default.GetLogFollowUps(log.LogId).Count.ToString() + " FollowUps";
                var CustomerDiscussion = Default.GetLogSummaryDiscussion(log.LogId);
                var KaizenOCR = log.Status.Status == "Closed" ? "Yes" : "No";
                var Satisfaction24 = Default.SatisfactionCheck(log.LogId, "24hr Satisfaction");
                var Satisfaction48 = Default.SatisfactionCheck(log.LogId, "48hr Satisfaction");
                var Satisfaction72 = Default.SatisfactionCheck(log.LogId, "72hr Satisfaction");
                var CloseDate = log.StatusClosedDate;
                var DaysTaken = "";
                if (String.IsNullOrEmpty(log.StatusClosedDate) == true)
                {
                    DaysTaken = "";
                }
                else
                {
                    DaysTaken = (DateTime.Parse(log.StatusSubmitDate).Date - DateTime.Parse(log.StatusClosedDate).Date).TotalDays.ToString();
                }

                IList<string> OneRecord = new List<string>
                {
                    log.LogId.ToString(), DateReceived, ReceivedMeans, Level, PIC, CRSupportMember, Registration, Model, Customer, CustomerCell, Complaint, Request, RootCause, CorrectiveAction, Status, StatusDate, ProgressUpdate, CustomerDiscussion, KaizenOCR, Satisfaction24, Satisfaction48, Satisfaction72, CloseDate
                };

                Records.Add(OneRecord);

            }
            
            return Page();
        }
    }
}