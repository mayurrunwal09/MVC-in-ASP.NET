using Microsoft.EntityFrameworkCore;

namespace ModelPopupOnetoMany.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options ): base(options  ) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasOne(d => d.Student)
                .WithMany(d => d.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .IsRequired();

            modelBuilder.Entity<Enrollment>()
                .HasOne(d => d.Course)
                .WithMany(d=>d.Enrollments)
                .HasForeignKey(d=>d.CourseId)
                .IsRequired();
        }
    }
}
