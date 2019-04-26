using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CoreLms.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreLms.Pages
{
    public class EmployeeActionModel : PageModel
    {
        private AppDbContext _context;

        public EmployeeActionModel(AppDbContext context){
            _context = context;
        }

        [BindProperty]
        public ICollection<Leave> MyLeaves {get; set;}

        [BindProperty]
        public ICollection<LeaveRequest> MyLeaveRequest {get; set;}

        public bool searchCompleted {get; set;}

        public string SearchAction {get; set;}

        public void OnGet(string action)
        {
            determineAction(action);
        }

        public async Task<IActionResult> OnPostAsync(int? id, string command)
        {
            if(id!=null) {
                if("Get Leave Details".Equals(command)) {
                MyLeaves = _context.Leave
                                    .Include(x=>x.Employee)
                                    .Include(x=>x.LeaveTypes)
                                    .Where(x=> x.EmployeeId == id).ToList();
                determineAction(command);
                }
                else if ("Search Requests".Equals(command)) {
                    MyLeaveRequest = _context.LeaveRequest
                                    .Include(x=>x.LeaveTypes)
                                    .Include(x=>x.Requestor)
                                    .Where(x=> x.RequestorId == id).ToList();
                determineAction(command);
                }
                else if ("Create Leave Request".Equals(command)) {
                    return RedirectToPage("./LeaveRequestSubmission", new {id = id});
                }
                searchCompleted = true;
            }
            else {
                determineAction(command);
            }
            return Page();
        }

        public void determineAction(string action) {
            if ("SearchMyRequests".Equals(action) || "Search Requests".Equals(action)){
                SearchAction = "Search Requests" ;
            }
            else if ("SearchMyLeaves".Equals(action) || "Get Leave Details".Equals(action)) {
                SearchAction = "Get Leave Details" ;
            }
            else if("NewLeaveRequest".Equals(action)) {
                SearchAction = "Create Leave Request" ;
            }
        }
    }
}