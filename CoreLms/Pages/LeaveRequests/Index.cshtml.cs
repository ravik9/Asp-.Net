using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreLms.Models;

namespace CoreLms.Pages.LeaveRequests
{
    public class IndexModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public IndexModel(CoreLms.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<LeaveRequest> LeaveRequest { get;set; }

        public async Task OnGetAsync()
        {
            LeaveRequest = await _context.LeaveRequest
                .Include(l => l.LeaveTypes)
                .Include(l => l.Requestor).ToListAsync();
        }
    }
}
