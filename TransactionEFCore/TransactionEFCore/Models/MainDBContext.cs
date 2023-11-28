using Microsoft.EntityFrameworkCore;

namespace TransactionEFCore.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Transaction>Transactions { get; set; }
    }
}
