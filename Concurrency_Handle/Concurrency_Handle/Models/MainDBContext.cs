using Microsoft.EntityFrameworkCore;

namespace Concurrency_Handle.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options):base(options) { }
        public DbSet<Department> Departments { get; set; }
    }

}
