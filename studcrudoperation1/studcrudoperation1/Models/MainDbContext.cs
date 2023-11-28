using Microsoft.EntityFrameworkCore;

namespace studcrudoperation1.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Student> Students { get; set; }
    }
}
