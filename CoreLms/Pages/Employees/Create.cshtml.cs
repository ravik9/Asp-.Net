using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreLms.Models;

namespace CoreLms.Pages.Employees
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
        ViewData["SupervisorId"] = new SelectList(_context.Employee, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["SupervisorId"] = new SelectList(_context.Employee, "Id", "Id");
                return Page();
            }

            _context.Employee.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}