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

namespace GroupCCP.Pages.site.Admin.Logs
{
    public class EditModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public EditModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ComplaintLogDetail ComplaintLogDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ComplaintLogDetail = await _context.ComplaintLogDetail
                .Include(c => c.Brands)
                .Include(c => c.Customers)
                .Include(c => c.Level)
                .Include(c => c.Means)
                .Include(c => c.Priority)
                .Include(c => c.StaffAccount)
                .Include(c => c.Status).FirstOrDefaultAsync(m => m.LogId == id);

            if (ComplaintLogDetail == null)
            {
                return NotFound();
            }
           ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId");
           ViewData["LogCustomerId"] = new SelectList(_context.ComplaintCustomerInfo, "CustomerId", "CustomerId");
           ViewData["LogLevelId"] = new SelectList(_context.Level, "LevelId", "LevelName");
           ViewData["LogMeansId"] = new SelectList(_context.ComplaintReceiveMeans, "MeansId", "Means");
           ViewData["PriorityId"] = new SelectList(_context.Priority, "PriorityId", "PriorityId");
           ViewData["StaffId"] = new SelectList(_context.StaffAccount, "AccountId", "UserId");
           ViewData["LogStatusId"] = new SelectList(_context.ComplaintLogStatus, "StatusId", "Status");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ComplaintLogDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintLogDetailExists(ComplaintLogDetail.LogId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ComplaintLogDetailExists(int id)
        {
            return _context.ComplaintLogDetail.Any(e => e.LogId == id);
        }
    }
}
