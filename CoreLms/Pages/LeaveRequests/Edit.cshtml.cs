using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreLms.Models;

namespace CoreLms.Pages.LeaveRequests
{
    public class EditModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public EditModel(CoreLms.Models.AppDbContext context)
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
           ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Id");
           ViewData["RequestorId"] = new SelectList(_context.Employee, "Id", "Designation");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LeaveRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveRequestExists(LeaveRequest.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequest.Any(e => e.Id == id);
        }
    }
}
