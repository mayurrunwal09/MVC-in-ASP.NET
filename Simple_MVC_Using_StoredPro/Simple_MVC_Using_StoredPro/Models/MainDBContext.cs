using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Simple_MVC_Using_StoredPro.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options):base(options) { }
        public DbSet<Customer> Customers { get; set; }  

        public IQueryable<Customer> SearchCustomer(string ContactName)
        {
            SqlParameter pContactName = new SqlParameter("@ContactName", ContactName);

            return this.Customers.FromSqlRaw("EXECUTE Customers_SearchCustomers @ContactName", pContactName);
        }

        public async  Task <string> AddCustomer(int CustomerID, string ContactName, string City, string Country)
        {
            var  pCustomerID = new SqlParameter("@CustomerID", CustomerID);
            var pContactNAme = new SqlParameter("@ContactName", ContactName);
            var pCity = new SqlParameter("@City", City);
            var pCountry = new SqlParameter("@Country", Country);

            var res = await  this.Database.ExecuteSqlRawAsync("EXECUTE Customer_InsertCustomer @CustomerID,@ContactName,@City,@Country"
                , pCustomerID, pContactNAme, pCity, pCountry);

            return res == 1? "Success" : "Error";
            
        }

        public async Task<string> UpdateCustomer(int CustomerID, string ContactName, string City, string Country)
        {
            var pCustomerID = new SqlParameter("@CustomerID", CustomerID);
            var pContactNAme = new SqlParameter("@ContactName", ContactName);
            var pCity = new SqlParameter("@City", City);
            var pCountry = new SqlParameter("@Country", Country);

            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE Customer_UpdateCsutomer @CustomerID,@ContactName,@City,@Country"
                , pCustomerID, pContactNAme, pCity, pCountry);

            return res == 1 ? "Success" : "Error";

        }

        public async Task<string> DeleteCustomer(int CustomerID)
        {
            var pCustomerID = new SqlParameter("@CustomerID", CustomerID);

            var result = await Database.ExecuteSqlRawAsync("EXEC Customer_DeleteCustomer @CustomerID", pCustomerID);

            return result == 1 ? "Success" : "Error: CustomerID with the provided CustomerID does not exist.";
        }

    }
}
