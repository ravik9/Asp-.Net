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
    public class IndexModel : PageModel
    {
        private readonly CoreLms.Models.AppDbContext _context;

        public IndexModel(CoreLms.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Leave> Leave { get;set; }

        public async Task OnGetAsync()
        {
            Leave = await _context.Leave
                .Include(l => l.Employee)
                .Include(l => l.LeaveTypes).ToListAsync();
        }
    }
}
