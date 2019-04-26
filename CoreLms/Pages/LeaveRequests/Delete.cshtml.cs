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
    public class DeleteModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public DeleteModel(CoreLms.Models.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveRequest = await _context.LeaveRequest.FindAsync(id);

            if (LeaveRequest != null)
            {
                _context.LeaveRequest.Remove(LeaveRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
