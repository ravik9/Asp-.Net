using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreLms.Models;

namespace CoreLms.Pages.LeaveTypes
{
    public class EditModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public EditModel(CoreLms.Models.AppDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LeaveType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(LeaveType.Id))
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

        private bool LeaveTypeExists(int id)
        {
            return _context.LeaveType.Any(e => e.Id == id);
        }
    }
}
