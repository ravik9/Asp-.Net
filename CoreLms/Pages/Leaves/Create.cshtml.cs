using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreLms.Models;

namespace CoreLms.Pages.Leaves
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
        ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id");
        ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Leave Leave { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id");
                ViewData["LeaveTypeId"] = new SelectList(_context.LeaveType, "Id", "Name");
                return Page();
            }

            _context.Leave.Add(Leave);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}