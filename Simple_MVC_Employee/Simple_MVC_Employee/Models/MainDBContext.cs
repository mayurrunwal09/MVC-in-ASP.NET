using Microsoft.EntityFrameworkCore;

namespace Simple_MVC_Employee.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options):base(options) { }

        public DbSet<Employee> Employees { get; set;}

        
    }
}
