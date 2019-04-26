using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreLms.Pages
{
    public class LeaveRequestSubmissionModel : PageModel
    {
        private AppDbContext _context;
        public LeaveRequestSubmissionModel(AppDbContext context){
            _context = context;
        }
        [BindProperty]
        public LeaveRequest LeaveRequest {get; set;}
        

        public Employee Emp{get; set;}
        [BindProperty]

        public Leave Leave{get; set;}
        [BindProperty]

        public int onid{get; set;}
        public IActionResult OnGet(int Id)
        {

            Emp = _context.Employee.Where(x => x.Id==Id).FirstOrDefault();
            onid = Id;

            if(Emp == null){
                return NotFound();
            }
            else{
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Name");
            //LeaveRequest.RequestorId = Id;
            return Page();
            }
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();
                ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Name");
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
            _context.Attach(Leave).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}