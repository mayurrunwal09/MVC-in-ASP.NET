using Microsoft.EntityFrameworkCore;

namespace EmpAndDep_Relations_CRUD.Models.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne<Department>(e => e.Department)
                .WithMany(e => e.employees)
                .HasForeignKey(e => e.Department_Id)
                .OnDelete(DeleteBehavior.Cascade);
                //OnDelete Cascade automatically delete child row
        }

        public DbSet<Employee> Employees { get; set;}
        public DbSet<Department> Department { get;set;}
    }
}
