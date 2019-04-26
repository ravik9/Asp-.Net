using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreLms.Pages
{
    public class ManagePendingRequestsModel : PageModel
    {
        private AppDbContext _context;

        public ManagePendingRequestsModel(AppDbContext context) {
            _context = context;            
        }

        [BindProperty]
        public ICollection<LeaveRequest> PendingLeaveRequests {get; set;}

        [BindProperty]
        public ICollection<LeaveRequest> ApprovedLeaves {get; set;}

         public void OnGetAsync()
        {
            PendingLeaveRequests = _context.LeaveRequest
                                            .Include(x=>x.Requestor)
                                            .Include(x=>x.LeaveTypes)
                                            .Where(x=> x.RequestStatus == "Pending")
                                            .ToList();

            ApprovedLeaves = _context.LeaveRequest
                                            .Include(x=>x.Requestor)
                                            .Include(x=>x.LeaveTypes)
                                            .Where(x=> x.RequestStatus == "Approved")
                                            .ToList();

        } 
    }
}