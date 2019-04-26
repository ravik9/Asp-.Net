using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreLms.Models;

namespace CoreLms.Pages.Leaves
{
    public class DetailsModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public DetailsModel(CoreLms.Models.AppDbContext context)
        {
            _context = context;
        }

        public Leave Leave { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Leave = await _context.Leave
                .Include(l => l.Employee)
                .Include(l => l.LeaveTypes).FirstOrDefaultAsync(m => m.Id == id);

            if (Leave == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
