using Microsoft.EntityFrameworkCore;
using OneToMany1.Models;

namespace OneToMany1.Data
{
    public class OneToManyDBContext : DbContext
    {
        public OneToManyDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UpdateEmployee> UpdateEmployees { get; set; }
        public DbSet<UpdateDepartment> UpdateDepartments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the one-to-many relationship
            modelBuilder.Entity<Employee>()
                .HasOne<Department>(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.Deptid)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
