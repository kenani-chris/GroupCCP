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
using NuGet.Packaging;

namespace GroupCCP.Pages.site

{
    public class Defaults
    {
        public readonly GroupCCP.Data.ApplicationDbContext _context;
        public StaffAccount Staff { get; set; }
        public IList<RoleAssignment> AssignedRoles { get; set; }
        public IList<PermissionAssignment> PermissionAssignments { get; set; }
        public Defaults(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Method to determine whether user has valid staff account
        public bool UserIsStaff(string user, int CompanyId)
        {
            Staff = _context.StaffAccount
                .Where(c => c.IsActive == true)
                .Where(c => c.CompanyId == CompanyId)
                .Where(c => c.User.UserName == user)
                .FirstOrDefault();

            if (Staff == null)
                return false;
            else
            {
                return true;
            }
        }

        // Method to get user staff account
        public StaffAccount GetStaffAccount(string user, int CompanyId)
        {
            return _context.StaffAccount
                .Where(c => c.IsActive == true)
                .Where(c => c.CompanyId == CompanyId)
                .Where(c => c.User.UserName == user)
                .FirstOrDefault();
        }
        
        // Method to determine log category
        public string GetLogType (int LogTypeId)
        {
            return LogTypeId switch
            {
                1 => "Submitted",
                2 => "My",
                3 => "Escallated",
                4 => "Level Down",
                _ => "Unknown",
            };
        }

        // Method to determine log permission entity required by a log type
        public string GetLogPermissionEntity(int LogTypeId, string Entity)
        {
            string LogType = GetLogType(LogTypeId);
            string LogPermissionEntity = Entity + " - " + LogType + " Complaints";
            return LogPermissionEntity;
        }

        // Method to determine if  staff account has page permission
        public bool StaffHasPermission(StaffAccount Staff, string Entity, string Permission)
        {
            var HasPerm = false;
            if (Staff == null || Entity == null || Permission == null)
            {
                HasPerm = true;
            }
            else
            {
                if (Staff.IsSuperUser)
                {
                    HasPerm = true;
                }
                else
                {
                    AssignedRoles = _context.RoleAssignment
                        .Include(c => c.Staff)
                        .Include(c => c.Roles)
                        .Where(c => c.Roles.CompanyId == Staff.CompanyId)
                        .Where(c => c.Staff == Staff).ToList();
                    if (AssignedRoles != null)
                    {
                        foreach (var AssignedRole in AssignedRoles)
                        {
                            var Role = AssignedRole.Roles;
                            var PermAssignments = _context.PermissionAssignment
                                .Include(c => c.Permissions)
                                .Where(c => c.Roles == Role).ToList();

                            if (PermAssignments != null)
                            {
                                foreach (var PermAssignment in PermAssignments)
                                {
                                    var Perm = PermAssignment.Permissions;
                                    if (Perm.Permission == Permission && Perm.Entity == Entity)
                                    {
                                        HasPerm = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return HasPerm;
        }

        // method to return logs per given logtype
        public IList<ComplaintLogDetail> GetLog(int LogTypeId, int StaffId)
        {
            if(LogTypeId == 1)
            {
                return _context.ComplaintLogDetail
                    .Include(c => c.ComplaintVehicleInfo)
                    .Include(c => c.Status)
                    .Include(c => c.Means)
                    .Include(c => c.Level)
                    .Include(c => c.Customers)
                    .Include(c => c.Priority)
                    .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                    .Where(c => c.StaffId == StaffId).ToList();
            }
            else if(LogTypeId == 2)
            {
                return GetAssignedLogs(StaffId, "Assignment");
            }
            else if(LogTypeId == 3)
            {
                return GetAssignedLogs(StaffId, "Escallation");
            }
            else if (LogTypeId == 4)
            {
                return GetLevelDownLogs(StaffId);
            }
            else
            {
                return null;
            }
        }

        // method to get logs assigned to someone
        public IList<ComplaintLogDetail> GetAssignedLogs(int StaffId, string AssignemntType)
        {
            IList<ComplaintLogDetail> AssignedLogs = new List<ComplaintLogDetail>();
            
            var Assignments = _context.ComplaintAssignment
                .Include(c => c.Staff)
                .Where(c => c.StaffAssigned == StaffId)
                .Where(c => c.AssignmentType == AssignemntType)
                .ToList();
            var AllAssignments = _context.ComplaintAssignment
                .Include(c => c.Staff)
                .Include(c => c.Log)
                .Include(c => c.Log)
                .ThenInclude(c => c.ComplaintVehicleInfo)
                .Include(c => c.Log)
                .ThenInclude(c => c.Customers)
                .Include(c => c.Log)
                .ThenInclude(c => c.Means)
                .Include(c => c.Log)
                .ThenInclude(c => c.Status)
                .Include(c => c.Log)
                .ThenInclude(c => c.Level)
                .Where(c => c.StaffAssigned == StaffId)
                .Where(c => c.AssignmentType == AssignemntType)
                .ToList();

            foreach (var Assignment in Assignments)
            {
                var Log = Assignment.Log;
                var LogAssignments = _context.ComplaintAssignment
                    .Where(c => c.LogId == Log.LogId)
                    .Where(c => c.AssignmentId > Assignment.AssignmentId)
                    .ToList();
                if(LogAssignments.Count > 0)
                {
                    AllAssignments.Remove(Assignment);
                }
            }

            foreach(var Assignment in AllAssignments)
            {
                AssignedLogs.Add(Assignment.Log);
            }

            return AssignedLogs;
        }

        // method to get all logs level down
        public IList<ComplaintLogDetail> GetLevelDownLogs(int StaffId)
        {
            IList<Level> LevelsDown = GetLevelDown(StaffId);
            IList<ComplaintLogDetail> LogDetails = new List<ComplaintLogDetail>();
            if (LevelsDown != null)
            {
                foreach(var LevelDown in LevelsDown)
                {
                    Console.WriteLine(LevelDown.LevelName);
                    var Logs = _context.ComplaintLogDetail
                        .Include(c => c.ComplaintVehicleInfo)
                        .Include(c => c.Status)
                        .Include(c => c.Means)
                        .Include(c => c.Level)
                        .Include(c => c.Customers)
                        .Include(c => c.Priority)
                        .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                        .Where(c => c.Level == LevelDown)
                        .ToList();
                    if(Logs != null)
                    {
                        LogDetails.AddRange(Logs);
                    }
                }
            }
            return LogDetails;

        }
        public IList<ComplaintLogDetail> GetLevelAndLevelDownLogs(int StaffId)
        {
            IList<ComplaintLogDetail> LogDetails = new List<ComplaintLogDetail>();

            var AllMemberships = _context.LevelMemberships
                .Include(c => c.Level)
                .Where(c => c.LevelId == StaffId);
                

            if(AllMemberships.Any() == true)
            {
                var Memberships = AllMemberships.First();
                IList<Level> LevelsDown = GetLevelDown(StaffId);
                if (Memberships != null)
                {
                    LevelsDown.Add(Memberships.Level);
                }


                if (LevelsDown != null)
                {
                    foreach (var LevelDown in LevelsDown)
                    {
                        var Logs = _context.ComplaintLogDetail
                            .Include(c => c.ComplaintVehicleInfo)
                            .Include(c => c.Status)
                            .Include(c => c.Means)
                            .Include(c => c.Level)
                            .Include(c => c.Customers)
                            .Include(c => c.Priority)
                            .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                            .Where(c => c.Level == LevelDown)
                            .ToList();
                        if (Logs != null)
                        {
                            LogDetails.AddRange(Logs);
                        }
                    }
                }
            }
                
            return LogDetails;

        }

        public IList<Level> GetLevelDown(int StaffId)
        {
            IList<Level> LevelsDown = new List<Level>();
            var Staff = _context.StaffAccount.FirstOrDefault(c => c.AccountId == StaffId);
            var Memberships = _context.LevelMemberships
                .Include(c => c.Level)
                .Where(c => c.StaffId == StaffId)
                .ToList();
            foreach(var Membership in Memberships)
            {
                Console.WriteLine(Membership.MembershipId.ToString() + " - it appears we have this");
                RecursiveLevelDown(Membership.Level, LevelsDown);
            }

            return LevelsDown;
            
        }

        public void RecursiveLevelDown(Level level, IList<Level> Levels)
        {
            var ChildLevels = _context.Level.Where(c => c.ParentLevel == level).ToList();
            if(ChildLevels != null)
            {
                foreach (var child in ChildLevels)
                {
                    Levels.Add(child);
                    RecursiveLevelDown(child, Levels);
                }
            }
        }


        public StaffAccount GetStaffAssignedLog(int LogId)
        {
            var Staff = _context.ComplaintAssignment
                .Include(c => c.Staff).ThenInclude(c => c.User)
                .Where(c => c.LogId == LogId)
                .OrderByDescending(c => c.AssignmentId);
            if (Staff.Any() == true)
            {
                return Staff.FirstOrDefault().Staff;
            }
            else
            {
                return null;
            }
        }

        public IList<string> GetCorrectives(int LogId)
        {
            var Correctives = _context.ComplaintCorrectiveInfo
                .Where(c => c.LogId == LogId);

            IList<string> CorrectivesList = new List<string>();
            var RootCause = "";
            var CorrectiveAction = "";

            foreach (var Corrective in Correctives)
            {
                RootCause  += "\n" + Corrective.RouteCause;
                CorrectiveAction += "\n" + Corrective.CorrectiveAction;
            }

            CorrectivesList.Add(RootCause);
            CorrectivesList.Add(CorrectiveAction);

            return CorrectivesList;
        }

        public IList<ComplaintFollowUp> GetLogFollowUps(int LogId)
        {
            return _context.ComplaintFollowUp
                .Include(c => c.FollowUpCalls)
                .Where(c => c.LogId == LogId)
                .ToList();
        }

        public string GetLogSummaryDiscussion(int LogId)
        {
            var Discussion = "";
            foreach(var Summary in GetLogFollowUps(LogId))
            {
                Discussion = "\n" + Summary.FollowUpFeedback;
            }
            return Discussion;
        }

        public string SatisfactionCheck(int LogId, string Satisfaction)
        {
            var FollowUps = GetLogFollowUps(LogId).Where(c => c.FollowUpCalls.FollowUpType == Satisfaction);
            if(FollowUps  == null)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }

        }

    }
}
