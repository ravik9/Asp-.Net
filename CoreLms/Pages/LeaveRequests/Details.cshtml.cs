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
    public class DetailsModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public DetailsModel(CoreLms.Models.AppDbContext context)
        {
            _context = context;
        }

        public LeaveRequest LeaveRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveRequest = await _context.LeaveRequest
                .Include(l => l.LeaveTypes)
                .Include(l => l.Requestor).FirstOrDefaultAsync(m => m.Id == id);

            if (LeaveRequest == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
