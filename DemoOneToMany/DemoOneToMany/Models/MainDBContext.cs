using Microsoft.EntityFrameworkCore;

namespace DemoOneToMany.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options):base(options) { }  

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne<Department>(e=>e.department)
                .WithMany(e=>e.Employees)
                .HasForeignKey(e=>e.DepId)
                .OnDelete(DeleteBehavior.Cascade);

                
        }
    }
}
