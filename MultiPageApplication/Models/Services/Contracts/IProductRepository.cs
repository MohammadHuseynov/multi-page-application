using MultiPageApplication.Models.DomainModels.ProductAggregates;
using MultiPageApplication.Models.Services.Contracts.RepositoryFrameworks;

namespace MultiPageApplication.Models.Services.Contracts
{
    // It now inherits all the methods from IRepository for the 'Product' entity
    public interface IProductRepository : IRepository<Product>
    {
        //•	Do not add CRUD methods (AddAsync, SelectByIdAsync, SelectAllAsync, Update, Delete, SaveChanges) here—they are already inherited.

        //Leave IProductRepository empty unless you need product-specific queries. AddAsync only those methods that are unique to Product and not generic CRUD operations. This keeps your code clean and maintainable.

        //You only add methods here that are SPECIFIC to products.
        // For example:
        // List<Product> GetProductsByCategory(int categoryId);
        // Product GetProductByName(string name);
    }
}
