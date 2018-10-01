using System.Data.Entity;

namespace ElectionCalculatorDataAccess
{
    public class ElectionContext : DbContext
    {
        public ElectionContext() : base("ElectionDBStringCopy")
        {
        }

        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}