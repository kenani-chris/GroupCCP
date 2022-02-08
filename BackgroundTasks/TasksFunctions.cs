using GroupCCP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GroupCCP
{
    public class TasksFunctions
    {
        public readonly GroupCCP.Data.ApplicationDbContext _context;
        public ComplaintLogDetail LogDetail { get; set; }
        
        public TasksFunctions(GroupCCP.Data.ApplicationDbContext context)
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

        public IList<Models.ComplaintLogDetail> GetOverdueAssignedLogs()
        {
            var logs = _context.ComplaintLogDetail
                .Include(c => c.Status)
                .Include(c => c.Level).ThenInclude(c => c.LevelCategory).ThenInclude(c => c.Company)
                .Include(c => c.Priority)
                .Where(c => c.Status.Status == "Assigned")
                .ToList();
            IList<Models.ComplaintLogDetail> OverdueLogs = new List<Models.ComplaintLogDetail>();

            foreach (var log in logs)
            {
                var logPriority = log.Priority;
                DateTime nowTime = DateTime.Now;
                DateTime logTime = DateTime.Parse(log.StatusSubmitDate);
                float TimeDiff = (float)nowTime.Subtract(logTime).TotalHours;

                if (GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId) != null){
                    float AllowedHrs = GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId).OverdueAssignedHrs;
                    float ReminderHrs = GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId).OverdueAssignedReminderHrs;

                    var Staff = GetStaffAssignedLog(log.LogId).Staff;
                    DateTime ReminderTime = logTime.AddHours(ReminderHrs);
                    DateTime AllowedTime = logTime.AddHours(AllowedHrs);

                    if(nowTime >= ReminderTime || nowTime >= AllowedTime)
                    {
                        OverdueLogs.Add(log);
                    }
                    
                }
            }
            return OverdueLogs;
        }
       
        public IList<Models.ComplaintLogDetail> GetOverdueResolvedLogs()
        {
            var logs = _context.ComplaintLogDetail
                .Include(c => c.Status)
                .Include(c => c.Level).ThenInclude(c => c.LevelCategory).ThenInclude(c => c.Company)
                .Include(c => c.Priority)
                .Where(c => c.Status.Status == "Resolved")
                .ToList();
            IList<Models.ComplaintLogDetail> OverdueLogs = new List<Models.ComplaintLogDetail>();

            foreach (var log in logs)
            {
                var logPriority = log.Priority;
                DateTime nowTime = DateTime.Now;
                DateTime logTime = DateTime.Parse(log.StatusSubmitDate);
                float TimeDiff = (float)nowTime.Subtract(logTime).TotalHours;

                if (GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId) != null){
                    float AllowedHrs = GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId).OverdueResolvedHrs;
                    float ReminderHrs = GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId).OverdueResolvedReminderHrs;

                    var Staff = GetStaffAssignedLog(log.LogId).Staff;
                    DateTime ReminderTime = logTime.AddHours(ReminderHrs);
                    DateTime AllowedTime = logTime.AddHours(AllowedHrs);

                    if(nowTime >= ReminderTime || nowTime >= AllowedTime)
                    {
                        OverdueLogs.Add(log);
                    }
                    
                }
            }
            return OverdueLogs;
        }
       
        public ComplaintAssignment GetStaffAssignedLog(int LogId)
        {
            return _context.ComplaintAssignment
                .Include(c => c.Staff).ThenInclude(c => c.User)
                .Where(c => c.LogId == LogId)
                .OrderByDescending(c => c.AssignmentId)
                .FirstOrDefault();
        }
        public void EscallateLog(int LogId)
        {
            TasksFunctions tasksFunctions = new(_context);
            if(GetStaffAssignedLog(LogId) != null)
            {
                var Staff = GetStaffAssignedLog(LogId).Staff;
                if(GetStaffMembership(Staff.AccountId) != null)
                {
                    var Level = GetStaffMembership(Staff.AccountId).Level;
                    if(Level.ParentLevel != null)
                    {
                        LevelMembership ParentMembership = null;

                        if (GetLevelPIC(Level.ParentLevel.LevelId) == null)
                        {
                            RecurseUpForPIC(Level.ParentLevel, ParentMembership);
                        }
                        else
                        {
                            ParentMembership = GetLevelPIC(Level.ParentLevel.LevelId);
                        }

                        if(ParentMembership != null)
                        {
                            AssignOneLog(ParentMembership.StaffId, LogId, "Escallatiion");
                            tasksFunctions.WriteLog("Escallated Log " + LogId + " to " + ParentMembership.Staff.User.Email + " from " + Staff.User.Email);
                            tasksFunctions.AddNotification(Staff.AccountId, "Log " + LogId + " has been escallated to " + ParentMembership.Staff.User.Email + " from you");
                            tasksFunctions.AddNotification(ParentMembership.Staff.AccountId, "Log " + LogId + " has been escallated to you from " + Staff.User.Email);
                        }

                    }
                }
            }
        }
        public void AssignOneLog(int StaffId, int LogId, string AssignmentType)
        {
            ComplaintAssignment Assignment = new();
            Assignment.StaffAssigned = StaffId;
            Assignment.AssignmentType = AssignmentType;
            Assignment.LogId = LogId;
            Assignment.AssignmentDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

            _context.ComplaintAssignment.Add(Assignment);
            _context.SaveChanges();

        }
        public void RecurseUpForPIC(Level Level, LevelMembership levelMembership)
        {
            if(Level.ParentLevel != null)
            {
                if (GetLevelPIC(Level.LevelId) != null)
                {
                    levelMembership = GetLevelPIC(Level.LevelId);
                }
                else
                {
                    RecurseUpForPIC(Level, levelMembership);
                }
            }
            else
            {
                levelMembership = null;
            }
        }

        public LevelMembership GetStaffMembership(int StaffId)
        {
            return _context.LevelMemberships
                .Include(c => c.Level)
                .Include(c => c.Staff).ThenInclude(c => c.User)
                .Where(c => c.MembershipActive == true)
                .Where(c => c.StaffId == StaffId)
                .FirstOrDefault();
        }

        public Timelines GetTimelines(int CompanyId, int PriorityId)
        {
            return _context.Timelines
                .Where(c => c.CompanyId == CompanyId)
                .Where(c => c.PriorityId == PriorityId)
                .FirstOrDefault();
        }
        public bool IsReminderDone(int StaffId, int LogId)
        {
            var Reminder = _context.OverdueReminder
                .Where(c => c.LogId == LogId)
                .Where(c => c.StaffId == StaffId)
                .FirstOrDefault();
            if (Reminder != null)
            {
                return true;
            }
            else 
            { 
                return false;
            }
        }
        public void AddReminder(int StaffId, int LogId, string ReminderType)
        {
            OverdueReminder Reminder = new();
            Reminder.LogId = LogId;
            Reminder.StaffId = StaffId;
            Reminder.Reminder = "It's noted the status of Log " + LogId + " has not changed since it was assigned to you and will be escallated if action is not done";
            Reminder.IsSent = false;
            Reminder.MessageType = ReminderType;

            _context.OverdueReminder.Add(Reminder);
            _context.SaveChanges();
        }
        public void EmailStaff(string StaffEmail, string Title, string Message)
        {
            string email = "pms_notifier@ck-pms.com";
            string password = "EZed9t&gZ%=S6HQ@h7R6X&4";
           
            MailMessage msg = new();

            msg.From = new MailAddress(email);
            msg.To.Add(StaffEmail);
            msg.Subject = Title;
            msg.Body = "Dear " + StaffEmail + ", " +
                "<p>" + Message +"</p>"
                + "<hr>"
                + "<b>Do not reply to this email, for it is system generated</b>"
                + "<hr>"
                + "<p>Best regards,<br> Notifier GroupCCP";
            msg.IsBodyHtml = true;

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(email, password);
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(msg);
                WriteLog("Sent Notification Email to " + StaffEmail + " with title \"" + Title + "\"  and message  \"" + Message + "\"");
            }
            catch (Exception ex)
            {
                WriteLog("failed to Send Notification Email to " + StaffEmail + " with error \"" + ex.Message + "\"");
            }
            

        }

        public void AssignLogs()
        {
            TasksFunctions tasksFunctions = new(_context);
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
                        Console.WriteLine(PIC);

                        if (PIC != null)
                        {
                            var Staff = PIC.Staff;
                            ComplaintAssignment Assignment = new();
                            Assignment.AssignmentDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                            Assignment.LogId = Log.LogId;
                            Assignment.AssignmentType = "Assignment";
                            Assignment.Staff = Staff;
                            _context.ComplaintAssignment.Add(Assignment);
                            _context.SaveChanges();

                            LogDetail = _context.ComplaintLogDetail
                                .FirstOrDefault(c => c.LogId == Log.LogId);
                            LogDetail.Status = _context.ComplaintLogStatus.FirstOrDefault(m => m.Status == "Assigned");

                            _context.SaveChanges();
                            tasksFunctions.WriteLog("Log Assignment: Log " + Log.LogId + " has been auto assigned to "+ Staff.User.Email);
                            tasksFunctions.AddNotification(Staff.AccountId, "You have been assigned <b>Log " + Log.LogId + "</b>");
                        }
                    
                    }
                }
            }

            // Escallate overdue assigned logs
            var OverdueAssignedLogs = GetOverdueAssignedLogs();
            if (OverdueAssignedLogs != null)
            {
                foreach (var log in OverdueAssignedLogs)
                {
                    var logPriority = log.Priority;
                    DateTime nowTime = DateTime.Now;
                    DateTime logTime = DateTime.Parse(log.StatusSubmitDate);
                    if (GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId) != null)
                    {
                        float AllowedHrs = GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId).OverdueAssignedHrs;
                        float ReminderHrs = GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId).OverdueAssignedReminderHrs;

                        if (GetStaffAssignedLog(log.LogId) != null)
                        {
                            var Staff = GetStaffAssignedLog(log.LogId).Staff;
                            DateTime ReminderTime = logTime.AddHours(ReminderHrs);
                            DateTime AllowedTime = logTime.AddHours(AllowedHrs);

                            if (nowTime >= ReminderTime && nowTime < AllowedTime)
                            {
                                if (!IsReminderDone(Staff.AccountId, log.LogId))
                                {
                                    AddReminder(Staff.AccountId, log.LogId, "Reminder");
                                }
                            }
                            else if (nowTime >= AllowedTime)
                            {
                                EscallateLog(log.LogId);
                            }
                        }
                    }
                }
            }
            
            // Escallate overdue assigned logs
            var OverdueResolvedLogs = GetOverdueResolvedLogs();
            if (OverdueResolvedLogs != null)
            {
                foreach (var log in OverdueResolvedLogs)
                {
                    var logPriority = log.Priority;
                    DateTime nowTime = DateTime.Now;
                    DateTime logTime = DateTime.Parse(log.StatusSubmitDate);
                    if (GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId) != null)
                    {
                        float AllowedHrs = GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId).OverdueResolvedHrs;
                        float ReminderHrs = GetTimelines(log.Level.LevelCategory.CompanyId, logPriority.PriorityId).OverdueResolvedReminderHrs;

                        if (GetStaffAssignedLog(log.LogId) != null)
                        {
                            var Staff = GetStaffAssignedLog(log.LogId).Staff;
                            DateTime ReminderTime = logTime.AddHours(ReminderHrs);
                            DateTime AllowedTime = logTime.AddHours(AllowedHrs);

                            if (nowTime >= ReminderTime && nowTime < AllowedTime)
                            {
                                if (!IsReminderDone(Staff.AccountId, log.LogId))
                                {
                                    AddReminder(Staff.AccountId, log.LogId, "Reminder");
                                }
                            }
                            else if (nowTime >= AllowedTime)
                            {
                                EscallateLog(log.LogId);
                            }
                        }
                    }
                }
            }

        }

        public LevelMembership GetLevelPIC(int level)
        {
            return _context.LevelMemberships
                .Include(c => c.Level)
                .Include(c => c.Staff).ThenInclude(c => c.User)
                .Where(c => c.MembershipActive == true)
                .Where(c => c.LevelId == level)
                .Where(c => c.MembershipRole == "PIC")
                .FirstOrDefault();
        }

        // A method to write log to file
        public void WriteLog(string strLog)
        {
            StreamWriter log;
            FileInfo logFileInfo;
            FileStream fileStream;

            string logFilePath = "C:\\Logs\\GroupCCP\\";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            DirectoryInfo logDirInfo = new(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            strLog = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + " : " + strLog;
            log.WriteLine(strLog);
            log.Close();
        }

        public void AddNotification(int StaffId, string Message)
        {
            Notification notification = new();
            notification.StaffId = StaffId;
            notification.Message = Message;
            notification.IsSent = false;
            _context.Notification.Add(notification);
            _context.SaveChanges();
        }

        public bool StatusChange(int LogId, int Status, IList<string> Errors, int CompanyId)
        {
            var Log = _context.ComplaintLogDetail
                .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                .Include(c => c.Priority)
                .Include(c => c.ComplaintVehicleInfo)
                .Include(c => c.Customers)
                .Include(c => c.Level)
                .Include(c => c.Means)
                .FirstOrDefault(c => c.LogId == LogId);
            bool StatusCanChange = false;

            if (Log != null)
            {
                if (Status == 3 || Status == 2 || Status == 1 || Status == 6 || Status == 7)
                {
                    StatusCanChange = true;
                }
                if (Status == 4)
                {
                    var FollowUpDone = CheckFollowUpDone(LogId, Errors, CompanyId);
                    var CorrectiveDone = CheckCorrective(LogId, Errors);

                    if (FollowUpDone == true &&  CorrectiveDone == true)
                    {
                        StatusCanChange = true;
                    }
                }
                if (Status == 5)
                {
                    var FollowUpDone = CheckFollowUpDone(LogId, Errors, CompanyId);
                    var CorrectiveDone = CheckCorrective(LogId, Errors);
                    var AllFieldsDone = CheckAllFields(LogId, Errors);
                    var ResponsibilityDone = CheckResponsibility(LogId, Errors);

                    if(FollowUpDone  == true &&  CorrectiveDone == true && AllFieldsDone == true && ResponsibilityDone == true)
                    {
                        StatusCanChange = true;
                    }
                }

            }
            return StatusCanChange;
            
        }
        public bool CheckAllFields(int LogId, IList<string> ErrorList)
        {
            var Log = _context.ComplaintLogDetail
                .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                .Include(c => c.Priority)
                .Include(c => c.ComplaintVehicleInfo)
                .Include(c => c.Customers)
                .Include(c => c.Level)
                .Include(c => c.Means)
                .FirstOrDefault(c => c.LogId == LogId);
            Console.WriteLine("the log number is " + Log.LogId.ToString());
            var TestPassed = true;
            if (string.IsNullOrEmpty(Log.CustomerComplaint))
            {
                ErrorList.Add("Log : The field Customer is Blank");
                TestPassed = false;
            }
            if (string.IsNullOrEmpty(Log.CustomerRequest))
            {
                ErrorList.Add("Log : The field Customer is Blank");
                TestPassed = false;
            }

            return TestPassed;
        }


        public bool CheckFollowUpDone(int LogId, IList<string> ErrorList, int CompanyId)
        {
            var Log = _context.ComplaintLogDetail
                .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                .Include(c => c.Priority)
                .Include(c => c.ComplaintVehicleInfo)
                .Include(c => c.Customers)
                .Include(c => c.Level)
                .Include(c => c.Means)
                .FirstOrDefault(c => c.LogId == LogId);
            var FollowUps = _context.FollowUpCalls
                .Where(c => c.CompanyId == CompanyId)
                .Where(c => c.FollowUpMandatory == true)
                .ToList();
            var DoneFollowUps = _context.ComplaintFollowUp
                .Where(c => c.LogId == LogId);

            var TestPassed = true;
            if (DoneFollowUps.Any() == false)
            {
                TestPassed = false;
                ErrorList.Add("FollowUp : No followups done");
            }
            else
            {
                foreach(var FollowUp in FollowUps)
                {
                    var FollowUPPresent = DoneFollowUps.Where(c => c.FollowUpTypeId == FollowUp.FollowUpId);
                    if(FollowUPPresent.Any() == false)
                    {
                        TestPassed = false;
                        ErrorList.Add("FollowUp : One or more required Followups are not submitted");
                    }

                }
            }

            return TestPassed;

        }

        public bool CheckCorrective(int LogId, IList<string> ErrorList)
        {
            var Log = _context.ComplaintLogDetail
                .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                .Include(c => c.Priority)
                .Include(c => c.ComplaintVehicleInfo)
                .Include(c => c.Customers)
                .Include(c => c.Level)
                .Include(c => c.Means)
                .FirstOrDefault(c => c.LogId == LogId);
            var Correctives = _context.ComplaintCorrectiveInfo
                .Where(c => c.LogId == LogId)
                .Any();
            bool TestPassed = true;
            if(Correctives == false)
            {
                TestPassed = false;
                ErrorList.Add("Correctives : Corrective Information for the Log has not been submitted");
            }

            return TestPassed;
        }

        public bool CheckResponsibility(int LogId, IList<string> ErrorList)
        {
            var Log = _context.ComplaintLogDetail
               .Include(c => c.StaffAccount).ThenInclude(c => c.User)
               .Include(c => c.Priority)
               .Include(c => c.ComplaintVehicleInfo)
               .Include(c => c.Customers)
               .Include(c => c.Level)
               .Include(c => c.Means)
               .FirstOrDefault(c => c.LogId == LogId);
            bool TestPassed = true;

            var CustomerResponsibility = _context.ComplaintResponsibility
                .Where(c => c.LogId == LogId)
                .Where(c => c.ResponsibilityPIC == "Customer")
                .Any();
            
            if (CustomerResponsibility == false)
            {
                TestPassed = false;
                ErrorList.Add("Responsibility : Customer responsibility Information for the Log has not been submitted");
            }

            var VehicleResponsibility = _context.ComplaintResponsibility
                .Where(c => c.LogId == LogId)
                .Where(c => c.ResponsibilityPIC == "Vehicle")
                .Any();
            if (VehicleResponsibility == false)
            {
                TestPassed = false;
                ErrorList.Add("Responsibility : Vehicle responsibility Information for the Log has not been submitted");
            }
            var DealerResponsibility = _context.ComplaintResponsibility
                .Where(c => c.LogId == LogId)
                .Where(c => c.ResponsibilityPIC == "Dealer")
                .Any();
            if (DealerResponsibility == false)
            {
                TestPassed = false;
                ErrorList.Add("Responsibility : Dealer responsibility Information for the Log has not been submitted");
            }

            return TestPassed;
        }

    }
}
