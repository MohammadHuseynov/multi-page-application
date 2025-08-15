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

        #region [- Insert() -]
        public async Task Insert(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            await _context.AddAsync(product);
        }
        #endregion

        #region [- Update() -]
        public Task Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            _context.Update(product);
            return Task.CompletedTask;
        }
        #endregion

        #region [- Delete() -]
        public Task Delete(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            _context.Remove(product);
            return Task.CompletedTask;
        }
        #endregion
          

        #region [- SelectAllAsync() -]
        public async Task<List<Product>> SelectAll()
        {
            return await _context.Product.AsNoTracking().ToListAsync();
        }

        #endregion

        #region [- SelectByIdAsync() -]
        public async Task<Product> SelectById(object id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            return await _context.Product.FindAsync(id);
        }
        #endregion
        
        #region [- SaveChangesAsync() -]
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion

    }
}
