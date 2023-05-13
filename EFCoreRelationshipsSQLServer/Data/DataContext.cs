using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsSQLServer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
    }
}
