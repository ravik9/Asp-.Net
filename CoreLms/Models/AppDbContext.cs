using Microsoft.EntityFrameworkCore;
using CoreLms.Models;

namespace CoreLms.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {
        }
        public DbSet<CoreLms.Models.Employee> Employee { get; set; }
        public DbSet<CoreLms.Models.Leave> Leave { get; set; }
        public DbSet<CoreLms.Models.LeaveRequest> LeaveRequest { get; set; }
        public DbSet<CoreLms.Models.LeaveType> LeaveType { get; set; }
    }
}