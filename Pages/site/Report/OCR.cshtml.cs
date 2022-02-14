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
    public class OCRModel : PageModel
    {
        private readonly GroupCCP.Data.ApplicationDbContext _context;

        public OCRModel(GroupCCP.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public ComplaintLogDetail ComplaintLogDetail { get; set; }
        public Company Company { get; set; }
        public string PageTitle { get; set; }
        public bool StaffHasPerm { get; set; }
        public bool RoleCreatePerm { get; set; }
        public StaffAccount StaffAccount { get; set; }
        public string PermissionRequired { get; set; }
        public string PermissionEntity { get; set; }
        public IList<string> OCRValues { get; set; }
        public IList<IList<string>> Records { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CompanyId, int? LogId)
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
                ComplaintLogDetail = await _context.ComplaintLogDetail
                    .Include(c => c.StaffAccount).ThenInclude(c => c.User)
                    .Include(c => c.Status)
                    .Include(c => c.Priority)
                    .Include(c => c.ComplaintVehicleInfo)
                    .Include(c => c.Customers)
                    .Include(c => c.Level)
                    .Include(c => c.Means)
                    .FirstOrDefaultAsync(c => c.LogId == LogId);
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
            PageTitle = "Report - Logs";
            var Correctives = await _context.ComplaintCorrectiveInfo
                .Include(c => c.ComplaintProductComponent)
                .Where(c => c.LogId == LogId)
                .ToListAsync();

            var CustomerResponsibility = await _context.ComplaintResponsibility
                .Where(c => c.LogId == LogId)
                .Where(c => c.ResponsibilityPIC == "Customer")
                .ToListAsync();

            var VehicleResponsibility = await _context.ComplaintResponsibility
                .Where(c => c.LogId == LogId)
                .Where(c => c.ResponsibilityPIC == "Vehicle")
                .ToListAsync();
            var DealerResponsibility = await _context.ComplaintResponsibility
                .Where(c => c.LogId == LogId)
                .Where(c => c.ResponsibilityPIC == "Dealer")
                .ToListAsync();

            var FollowUps = await _context.ComplaintFollowUp
                .Where(c => c.LogId == LogId)
                .ToListAsync();

            var ProductComponent = "";
            var SubComponent = "";
            var RootCause = "";
            var CustomerExplanantion = "";
            var SuggestedCorrection = "";

            var CustomerReponsibilityLevel = "";
            var CustomerReponsibiltyReason = "";

            var VehicleReponsibilityLevel = "";
            var VehicleReponsibiltyReason = "";

            var DealerReponsibilityLevel = "";
            var DealerReponsibiltyReason = "";

            var DiagnosisTimeTaken = "";
            var RectifyTimeTaken = "";
            double PartsCost = 0;
            double OtherCost = 0;
            var FollowUpDates = "";

            foreach(var Corrective in Correctives)
            {
                ProductComponent += Corrective.ComplaintProductComponent.ProductComponent + "<br>";
                SubComponent += Corrective.ComplaintSubComponent + "<br>";
                RootCause += Corrective.RouteCause + "<br>";
                CustomerExplanantion += Corrective.CorrectiveCustomerExplanation + "<br>";
                SuggestedCorrection += Corrective.CorrectiveAction + "<br>";
                DiagnosisTimeTaken += Corrective.CorrectiveDiagnosisTimeTaken + "<br>";
                RectifyTimeTaken += Corrective.CorrectiveRectifyTimeTaken + "<br>";
                PartsCost += Corrective.CorrectivePartsCostKSH;
                OtherCost += Corrective.CorrectiveOtherCostKSH;
            }

            foreach(var Responsibility in CustomerResponsibility)
            {
                CustomerReponsibilityLevel += Responsibility.ResponsibilityLevel + "<br>";
                CustomerReponsibiltyReason += Responsibility.ResponsibilityReason + "<br>";
            }
            foreach(var Responsibility in VehicleResponsibility)
            {
                VehicleReponsibilityLevel += Responsibility.ResponsibilityLevel + "<br>";
                VehicleReponsibiltyReason += Responsibility.ResponsibilityReason + "<br>";
            }
            foreach(var Responsibility in DealerResponsibility)
            {
                DealerReponsibilityLevel += Responsibility.ResponsibilityLevel + "<br>";
                DealerReponsibiltyReason += Responsibility.ResponsibilityReason + "<br>";
            }

            foreach(var FollowUp in FollowUps)
            {
                FollowUpDates += FollowUp.FollowUpDate + "<br>";
            }

            OCRValues = new List<string>();
            OCRValues.Add(ProductComponent);
            OCRValues.Add(SubComponent);
            OCRValues.Add(RootCause);
            OCRValues.Add(CustomerReponsibilityLevel);
            OCRValues.Add(CustomerReponsibiltyReason);
            OCRValues.Add(VehicleReponsibilityLevel);
            OCRValues.Add(VehicleReponsibilityLevel);
            OCRValues.Add(DealerReponsibilityLevel);
            OCRValues.Add(DealerReponsibiltyReason);
            OCRValues.Add(SuggestedCorrection);
            OCRValues.Add(CustomerExplanantion);
            OCRValues.Add(RootCause);
            OCRValues.Add(FollowUpDates);
            OCRValues.Add(DiagnosisTimeTaken);
            OCRValues.Add(RectifyTimeTaken);
            OCRValues.Add(PartsCost.ToString() + " ksh");
            OCRValues.Add(OtherCost.ToString() + " ksh");
            return Page();
        }
    }
}