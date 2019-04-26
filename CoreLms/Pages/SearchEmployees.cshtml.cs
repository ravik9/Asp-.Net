using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreLms.Pages
{
    public class SearchEmployeesModel : PageModel
    {
        private AppDbContext _context;
        public SearchEmployeesModel(AppDbContext context){
            _context = context;
        }

        [BindProperty]
        public string Search { get; set; }

        public bool SearchCompleted { get; set; }

        public void OnGet()
        {
            
          SearchCompleted = false;

        }
        [BindProperty]
        public ICollection<Employee> SearchResults { get; set; }
        
        public void OnPost() {
            // PERFORM SEARCH
            if (string.IsNullOrWhiteSpace(Search)) {
                // EXIT EARLY IF THERE IS NO SEARCH TERM PROVIDED
                return;
            }
            SearchResults = _context.Employee.Include(y => y.Leaves).ThenInclude(y => y.LeaveTypes)
                                    .Where(x => x.FirstName.ToLower().Contains(Search.ToLower()))
                                    .ToList();
            if(SearchResults!=null) {
                SearchCompleted = true;
            }
        }
    }
}