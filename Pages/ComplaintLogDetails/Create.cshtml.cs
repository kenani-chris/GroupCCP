using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.ComplaintLogDetails
{
    public class CreateModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public CreateModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId");
        ViewData["LogCustomerId"] = new SelectList(_context.ComplaintCustomerInfo, "CustomerId", "CustomerId");
        ViewData["LogLevelId"] = new SelectList(_context.Level, "LevelId", "LevelName");
        ViewData["LogMeansId"] = new SelectList(_context.ComplaintReceiveMeans, "MeansId", "Means");
        ViewData["PriorityId"] = new SelectList(_context.Priority, "PriorityId", "PriorityId");
        ViewData["StaffId"] = new SelectList(_context.StaffAccount, "AccountId", "UserId");
        ViewData["LogStatusId"] = new SelectList(_context.ComplaintLogStatus, "StatusId", "Status");
            return Page();
        }

        [BindProperty]
        public ComplaintLogDetail ComplaintLogDetail { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ComplaintLogDetail.Add(ComplaintLogDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
