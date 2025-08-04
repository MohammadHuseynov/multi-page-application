using Microsoft.EntityFrameworkCore;
using MultiPageApplication.Models.DomainModels.ProductAggregates;
using MultiPageApplication.Models.Services.Contracts;


namespace MultiPageApplication.Models.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MultiPageApplicationDbContext _context;

        public ProductRepository(MultiPageApplicationDbContext context)
        {
            _context = context;
        }

      
        public async Task AddAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            await _context.Product.AddAsync(product);
        }

       
        public async Task<List<Product>> SelectAllAsync()
        {
            return await _context.Product.ToListAsync();
        }

    

        public async Task<Product> SelectByIdAsync(object id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            return await _context.Product.FindAsync(id);
        }

    
        public Task DeleteAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            _context.Product.Remove(product);
            return Task.CompletedTask;
        }

      
        public Task UpdateAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            _context.Product.Update(product);
            return Task.CompletedTask;
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
