using GroupCCP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GroupCCP.Pages.site
{
    public class WindowsService
    {
        
        public readonly GroupCCP.Data.ApplicationDbContext _context;
        public ComplaintAssignment ComplaintAssignment { get; set; }
        public ComplaintLogDetail LogDetail { get; set; }
        
        public WindowsService(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GroupCCP.Models.ComplaintLogDetail> ComplaintLogDetail { get; set; }

        public IList<Models.ComplaintLogDetail> GetOpenLogs()
        {
            return _context.ComplaintLogDetail
                    .Include(c => c.Status)
                    .Include(c => c.Means)
                    .Include(c => c.Level)
                    .Include(c => c.Customers)
                    .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                    .Where(c => c.Status.Status == "Open")
                    .ToList();           
        }

        public void EmailStaff(int StaffId, string Title, string Message)
        {
            StaffAccount Staff = _context.StaffAccount
                .Include(c => c.User)
                .FirstOrDefault(c => c.AccountId == StaffId);

            string email = "pms_notifier@ck-pms.com";
            string password = "EZed9t&gZ%=S6HQ@h7R6X&4";
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true,
            };

            smtpClient.Send(email, Staff.User.Email, Title, Message);
        }

        public void AssignLgs()
        {
            // Assign Open Logs
            var OpenLogs = GetOpenLogs();
            if (OpenLogs.Count > 0)
            {
                foreach(ComplaintLogDetail Log in OpenLogs)
                {
                    var Level = Log.LogLevelId;
                    if(Level > 0)
                    {
                        var PIC = GetLevelPIC(Level);

                        if (PIC != null)
                        {
                            var Staff = PIC.Staff;

                            ComplaintAssignment.AssignmentDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                            ComplaintAssignment.LogId = Log.LogId;
                            ComplaintAssignment.AssignmentType = "Assignment";
                            LogDetail = _context.ComplaintLogDetail
                                .FirstOrDefault(c => c.LogId == Log.LogId);
                            LogDetail.Status = _context.ComplaintLogStatus.FirstOrDefault(m => m.Status == "Assigned");
                            _context.ComplaintAssignment.Add(ComplaintAssignment);
                            _context.SaveChangesAsync();
                        }
                    }
                }
            }

            // Escalate Logs
        }

        public LevelMembership GetLevelPIC(int level)
        {
            return _context.LevelMemberships
                .Include(c => c.Level)
                .Include(c => c.Staff).ThenInclude(c => c.User)
                .Where(c => c.MembershipActive == true)
                .Where(c => c.LevelId == level)
                .Where(c => c.MembershipRole == "PIC")
                .First();
        }
    }
}
