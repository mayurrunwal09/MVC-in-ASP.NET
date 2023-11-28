using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Model_Popup_Using_StoredProcedure.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options): base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(d => d.Department)
                .WithMany(d=>d.Employees)
                .HasForeignKey(d => d.DepId)
                .IsRequired();
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Employee>().ToTable("Employee");
        }

        public IQueryable<Department> SearchDepartment(string Depname)
        {
            SqlParameter pDepname = new SqlParameter("@DepId", Depname);

            return this.Departments .FromSqlRaw("EXECUTE SearchDepartment @DepId", pDepname);
        }

        public async Task<string> InsertOrUpdateDepartment(int depId, string depName)
        {
            var depIdParameter = new SqlParameter("@Dep_ID", depId);
            var depNameParameter = new SqlParameter("@Dep_Name", depName);

            // Execute the stored procedure and return the result
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC InsertOrUpdateDepartment @Dep_ID, @Dep_Name",
                depIdParameter, depNameParameter);

            return result == 1 ? "S" : "E";
        }

        public async Task<string> DeleteDepartment(int DepId)
        {
            var pDepId = new SqlParameter("@DepId", DepId);

            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE DeleteDepartment @DepId", pDepId);
            return res == 1 ? "Success" : "Error";
        }
        /* public IQueryable<Employee> SearchEmployee(string Depname)
         {
             SqlParameter pDepname = new SqlParameter("@EmpId", Depname);

             return this.Employees.FromSqlRaw("EXECUTE SearchEmployee @EmpId", pDepname);
         }*/

        public async Task<string> InsertOrUpdateEmployee(int empId, string empName, string gender, string city,  int depId)
        {
            var empIdParameter = new SqlParameter("@Emp_ID", empId);
            var empNameParameter = new SqlParameter("@Emp_name", empName);
            var genderParameter = new SqlParameter("@Gender", gender);
            var cityParameter = new SqlParameter("@City", city);
          
            var depIdParameter = new SqlParameter("@Dep_ID", depId);

            // Execute the stored procedure and return the result
            var result = await Database.ExecuteSqlRawAsync(
                "EXEC InsertOrUpdateEmployee @Emp_ID, @Emp_name, @Gender, @City, @Sallary, @Dep_ID",
                empIdParameter, empNameParameter, genderParameter, cityParameter,  depIdParameter);

            return result == 1 ? "S" : "E";
        }

        public async Task<string> DeleteEmployee(int empId)
        {
            // Define the parameter for the stored procedure
            var empIdParam = new SqlParameter("@EmpId", empId);

            // Execute the stored procedure using raw SQL
            var res = await this.Database.ExecuteSqlRawAsync("EXEC DeleteEmployee @EmpId", empIdParam);
            return res == 1 ? "Success" : "Error";
        }
    }
}
