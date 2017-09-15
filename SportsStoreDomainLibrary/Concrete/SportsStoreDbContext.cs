using System.Data.Entity;

using SportsStoreDomainLibrary.Entities;

namespace SportsStoreDomainLibrary.Concrete
{
    public class SportsStoreDbContext: DbContext
    {
        public SportsStoreDbContext() : base("SportsStoreConnection") { }
        public DbSet<Product> Products { get; set; }
    }
}
