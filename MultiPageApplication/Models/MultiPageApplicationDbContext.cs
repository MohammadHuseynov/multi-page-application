using Microsoft.EntityFrameworkCore;
using MultiPageApplication.Models.DomainModels.ProductAggregates;

namespace MultiPageApplication.Models
{
    public class MultiPageApplicationDbContext : DbContext
    {
        public MultiPageApplicationDbContext()
        {

        }

        public MultiPageApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }


    }
}
