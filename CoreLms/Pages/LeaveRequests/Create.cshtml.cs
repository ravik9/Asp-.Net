using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreLms.Models;

namespace CoreLms.Pages.LeaveRequests
{
    public class CreateModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public CreateModel(CoreLms.Models.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Name");
        ViewData["RequestorId"] = new SelectList(_context.Employee, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public LeaveRequest LeaveRequest { get; set; }

        [BindProperty]
        public Leave Leave{get; set;}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Id");
                ViewData["RequestorId"] = new SelectList(_context.Employee, "Id", "Id");
                return Page();
            }
            TimeSpan? difference = LeaveRequest.EndDate - LeaveRequest.StartDate;
            LeaveRequest.NoOfDays = difference.Value.Days + 1;
            LeaveRequest.RequestDate = DateTime.Today;
            LeaveRequest.RequestStatus = "Pending";

            Leave = _context.Leave.Where(p => p.LeaveTypeId == LeaveRequest.
                                        LeaveTypeId && p.EmployeeId == LeaveRequest.RequestorId).First();
             if(Leave != null){
                 Leave.Balance = Leave.Balance - LeaveRequest.NoOfDays;
            }
            _context.LeaveRequest.Add(LeaveRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}