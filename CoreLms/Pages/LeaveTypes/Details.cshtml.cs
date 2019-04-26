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
    public class DetailsModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public DetailsModel(CoreLms.Models.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
