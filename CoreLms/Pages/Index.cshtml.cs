using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreLms.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;
        public int Cuurent_emp { get; set; }
        public int No_of_Supervisors { get; set; }
        public int No_of_LeaveReq { get; set; }
        public int No_of_ApprovedReq { get; set; }
        public int No_of_RejectedReq { get; set; }
        public int No_of_sickreq { get; set; }
        public IndexModel(CoreLms.Models.AppDbContext context){
            _context = context;
        }
        public void OnGet()
        {
                Cuurent_emp = _context.Employee
                                        .Where(x => x.LastDay == null).Count();
                                        
                No_of_Supervisors = _context.Employee
                                        .Where(x => x.IsSupervisor == true).Count();  

                No_of_LeaveReq = _context.LeaveRequest.Count();

                No_of_ApprovedReq = _context.LeaveRequest.Where(x => x.RequestStatus == "Approve").Count();

                No_of_RejectedReq = _context.LeaveRequest.Where(x => x.RequestStatus == "Reject").Count();

                No_of_sickreq = _context.LeaveRequest.Where(s => s.LeaveTypes.Name == "Sick").Count();

        }
    }
}
