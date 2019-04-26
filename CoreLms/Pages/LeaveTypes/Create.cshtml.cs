using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreLms.Models;

namespace CoreLms.Pages.LeaveTypes
{
    public class CreateModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public CreateModel(CoreLms.Models.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LeaveType LeaveType { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.LeaveType.Add(LeaveType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}