using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Employee_Stored_Procedure.Models
{
    public class MainDBContext:DbContext
    {
        public MainDBContext(DbContextOptions options):base(options ) { }
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
            SqlParameter pDepname = new SqlParameter("@DepName", Depname);

            return this.Department.FromSqlRaw("EXECUTE Search_Department @DepName", pDepname);
        }
        public async Task<string> AddDepartment(int DepId, string DepName)
        {
            var pDepId = new SqlParameter("@DepId", DepId);
            var pDepName = new SqlParameter("@DepName", DepName);

            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE Department_Insert @DepId,@DepName", pDepId, pDepName);
            return res == 1 ? "Success" : "Error";
        }
        /*public async Task<string> AddDepartment(int DepId, string DepName)
        {
            var sql = $"EXECUTE Department_Insert @DepId = {DepId}, @DepName = {DepName}";

            var result = await Database.ExecuteSqlRawAsync(sql);

            return result == 1 ? "Success" : "Error";
        }*/

        public async Task<string> UpdateDepartment(int DepId, string DepName)
        {
            var pDepId = new SqlParameter("@DepId", DepId);
            var pDepName = new SqlParameter("@DepName", DepName);

            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE Department_Update @DepId,@DepName", pDepId,pDepName);
            return res == 1 ? "Success" : "Error";
        }
        public async Task<string> DeleteDepartment(int DepId)
        {
            var pDepId = new SqlParameter("@DepId", DepId);

            var result = await Database.ExecuteSqlRawAsync("EXEC Department_Delete @DepId", pDepId);

            return result == 1 ? "Success" : "Error";
        }


        public IQueryable<Employee> SearchEmployee(string EmpId)
        {
            SqlParameter pEmpId = new SqlParameter("@EmpId", EmpId);

            return this.Employees.FromSqlRaw("EXECUTE Search_Employee @EmpId", pEmpId);
        }
        public async Task<string > AddEmployee(int EmpId,string EmpName,int Age,string MobileNo,DateTime DOB,int DepId)
        {
            var pEmpId = new SqlParameter("@EmpId", EmpId);
            var pEmpName = new SqlParameter("@EmpName", EmpName);
            var pAge = new SqlParameter("@Age", Age);
            var pMobileNo = new SqlParameter("@MobileNo", MobileNo);
            var pDOB = new SqlParameter("@DOB", DOB);
            var pDepartment = new SqlParameter("@DepId", DepId);
             var res = await   this.Database.ExecuteSqlRawAsync("EXECUTE Employee_Insert @EmpId,@EmpName,@Age,@MobileNo,@DOB,@DepId",
                 pEmpId,pEmpName,pAge,pMobileNo,pDOB,pDepartment);
            return res == 1 ? "Success" : "Error";
        }
        public async Task<string> UpdateEmployee(int EmpId, string EmpName, int Age, string MobileNo, DateTime DOB, int DepId)
        {
            var pEmpId = new SqlParameter("@EmpId", EmpId);
            var pEmpName = new SqlParameter("@EmpName", EmpName);
            var pAge = new SqlParameter("@Age", Age);
            var pMobileNo = new SqlParameter("@MobileNo", MobileNo);
            var pDOB = new SqlParameter("@DOB", DOB);
            var pDepartment = new SqlParameter("@DepId", DepId);
            var res =await  this.Database.ExecuteSqlRawAsync("EXECUTE Employee_Update @EmpId,@EmpName,@Age,@MobileNo,@DOB,@DepId",
                pEmpId, pEmpName, pAge, pMobileNo, pDOB, pDepartment);
            return res == 1 ? "Success" : "Error";
        }
        public async Task<string> DeleteEmployee(int EmpId)
        {
            var pEmpId = new SqlParameter("@EmpId", EmpId);

            var result = await Database.ExecuteSqlRawAsync("EXEC Employee_Delete @EmpId", pEmpId);

            return result == 1 ? "Success" : "Error: EmpId with the provided EmpId does not exist.";
        }

    }
}
