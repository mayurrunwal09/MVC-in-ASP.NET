using Microsoft.EntityFrameworkCore;
using newcorespp.Models.Domain;

namespace newcorespp.Data
{
    public class MVCDemoContext : DbContext
    {
        public MVCDemoContext(DbContextOptions options):base(options) 
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
