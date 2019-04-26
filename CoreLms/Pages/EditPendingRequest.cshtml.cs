using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreLms.Models;

namespace CoreLms.Pages
{
    public class EditPendingRequestModel : PageModel
    {
        private AppDbContext _context;

        public EditPendingRequestModel(AppDbContext context) {
            _context = context;
        }

        [BindProperty]
        public LeaveRequest LeaveRequest { get; set; }
        
        [BindProperty]
        public Employee Employee{get; set;}
        
        [BindProperty]
        public Leave Leave{get; set;}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveRequest = await _context.LeaveRequest
                                        .Include(l => l.LeaveTypes)
                                        .Include(l => l.Requestor)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (LeaveRequest == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string command)
        {
            /* (!ModelState.IsValid)
            {
                return Page();
            } */
            
            LeaveRequest = await _context.LeaveRequest
                                        .Include(l => l.LeaveTypes)
                                        .Include(l => l.Requestor)
                                        .FirstOrDefaultAsync(m => m.Id == LeaveRequest.Id);
            if(LeaveRequest != null) {
                if("Approve".Equals(command)) {
                    LeaveRequest.RequestStatus = command;
                    LeaveRequest.ApprRejDate = DateTime.Today;
                
                    _context.Attach(LeaveRequest).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else if("Reject".Equals(command)) {
                    LeaveRequest.RequestStatus = command;
                    LeaveRequest.ApprRejDate = DateTime.Today;

                    Leave = _context.Leave.Where(p => p.LeaveTypeId == LeaveRequest.LeaveTypeId && p.EmployeeId == LeaveRequest.RequestorId).First();
                    if(Leave != null) {Leave.Balance = Leave.Balance + LeaveRequest.NoOfDays;}
                    
                    _context.Attach(Leave).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    
                    _context.Attach(LeaveRequest).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToPage("./ManagePendingRequests");
            }
            return NotFound();
        }

    }
}