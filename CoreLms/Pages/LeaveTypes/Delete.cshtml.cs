using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreLms.Models;

namespace CoreLms.Pages.LeaveTypes
{
    public class DeleteModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public DeleteModel(CoreLms.Models.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LeaveType LeaveType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LeaveType = await _context.LeaveType.FirstOrDefaultAsync(m => m.Id == id);

            if (LeaveType == null)
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

            LeaveType = await _context.LeaveType.FindAsync(id);

            if (LeaveType != null)
            {
                _context.LeaveType.Remove(LeaveType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
