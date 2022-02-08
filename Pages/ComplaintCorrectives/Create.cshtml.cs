using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GroupCCP.Data;
using GroupCCP.Models;

namespace GroupCCP.Pages.ComplaintCorrectives
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
        ViewData["CorrectiveComponentId"] = new SelectList(_context.ComplaintProductComponent, "ProductID", "ProductID");
        ViewData["LogId"] = new SelectList(_context.ComplaintLogDetail, "LogId", "LogId");
        ViewData["StaffId"] = new SelectList(_context.StaffAccount, "AccountId", "UserId");
            return Page();
        }

        [BindProperty]
        public ComplaintCorrectiveInfo ComplaintCorrectiveInfo { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ComplaintCorrectiveInfo.Add(ComplaintCorrectiveInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
