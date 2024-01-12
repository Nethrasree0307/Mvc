using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class ApplicationDBContext : DbContext
    {     
        public DbSet<Employee> Employees{get;set;}=default!; //default to declare property as null
        public DbSet<Leave> LeaveDetails { get; set; }=default!;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           base.OnModelCreating(builder);
        }
    }

}
