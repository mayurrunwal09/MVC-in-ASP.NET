using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Employee_Dep_Stored_Procedure.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(d => d.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(d => d.DepId)
                .IsRequired();
        }

        public IQueryable<Department> SearchDepartment(string Depname)
        {
            SqlParameter pDepname = new SqlParameter("@SearchKeyword", Depname);

            return this.Department.FromSqlRaw("EXECUTE Department_Search @SearchKeyword", pDepname);
        }
      

        public async Task<string> AddDepartment(string DepName)
        {
            var pDepName = new SqlParameter("@DepName", DepName);

            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE Department_Insert @DepName", pDepName);
            return res == 1 ? "Success" : "Error";
        }



       
        public async Task<string> UpdateDepartment(int DepId, string NewDepName)
        {
            var pDepId = new SqlParameter("@DepID", DepId);
            var pNewDepName = new SqlParameter("@NewDepName", NewDepName);

            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE Department_Update @DepID, @NewDepName", pDepId, pNewDepName);
            return res == 1 ? "Success" : "Error";
        }

     

        public async Task<string> DeleteDepartment(int DepId)
        {
            var pDepId = new SqlParameter("@DepID", DepId);

            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE Department_Delete @DepID", pDepId);
            return res == 1 ? "Success" : "Error";
        }


        public async Task<string > AddEmployee(string empName, int age, string mobileNo, DateTime dob, int depId)
        {
            // Define the parameters for the stored procedure
            var empNameParam = new SqlParameter("@EmpName", empName);
            var ageParam = new SqlParameter("@Age", age);
            var mobileNoParam = new SqlParameter("@MobileNo", mobileNo);
            var dobParam = new SqlParameter("@DOB", dob);
            var depIdParam = new SqlParameter("@DepId", depId);

            // Execute the stored procedure using raw SQL
        var res = await this.Database.ExecuteSqlRawAsync("EXEC Insert_Employee @EmpName, @Age, @MobileNo, @DOB, @DepId",
                empNameParam, ageParam, mobileNoParam, dobParam, depIdParam);
            return res == 1 ? "Success" : "Error";
        }

        public async Task<string> UpdateEmployee(int DepId,string empName, int age, string mobileNo, DateTime dob, int depId)
        {
            // Define the parameters for the stored procedure
            var pDepId = new SqlParameter("EmpId",DepId);
            var empNameParam = new SqlParameter("@EmpName", empName);
            var ageParam = new SqlParameter("@Age", age);
            var mobileNoParam = new SqlParameter("@MobileNo", mobileNo);
            var dobParam = new SqlParameter("@DOB", dob);
            var depIdParam = new SqlParameter("@DepId", depId);

            // Execute the stored procedure using raw SQL
            var res = await this.Database.ExecuteSqlRawAsync("EXEC UpdateEmployee @EmpId, @EmpName, @Age, @MobileNo, @DOB, @DepId",
                    pDepId,empNameParam, ageParam, mobileNoParam, dobParam, depIdParam);
            return res == 1 ? "Success" : "Error";
        }
        public async Task<string > DeleteEmployee(int empId)
        {
            // Define the parameter for the stored procedure
            var empIdParam = new SqlParameter("@EmpId", empId);

            // Execute the stored procedure using raw SQL
         var res = await   this.Database.ExecuteSqlRawAsync("EXEC DeleteEmployee @EmpId", empIdParam);
            return res == 1 ? "Success" : "Error";
        }

        public IQueryable<Employee> SearchEmployee(string Depname)
        {
            SqlParameter pDepname = new SqlParameter("@SearchKeyword", Depname);

            return this.Employees.FromSqlRaw("EXECUTE EmployeeSearch @SearchKeyword", pDepname);
        }

    }
}
