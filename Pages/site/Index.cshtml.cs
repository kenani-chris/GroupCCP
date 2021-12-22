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
        public async Task<IActionResult> OnGetAsync(int? CompanyId)
        {
            if (CompanyId == null)
            {
                return NotFound();
            }
            Company = await _context.Company
                .Include(c => c.Group).FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
            StaffAccount = await _context.StaffAccount.Where(c => c.CompanyId == CompanyId).FirstOrDefaultAsync(c => c.User.UserName == User.Identity.Name);
            if (StaffAccount == null)
            {
                return NotFound(CompanyId);
            }
            MyLogs = await _context.ComplaintAssignment
                .Where(c => c.Staff == StaffAccount).ToListAsync();
            MyAssignedLogs = await _context.ComplaintAssignment
                .Include(c => c.Log).ThenInclude(c => c.Status)
                .Where(c => c.Staff == StaffAccount).ToListAsync();
            
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
                if (item.Log.Status.Status == "Wip")
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
