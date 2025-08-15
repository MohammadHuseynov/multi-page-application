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
            try
            {
                return await _context.Product.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Could not retrieve products from the database.", ex);
            }
        }

        #endregion

        #region [- SelectByIdAsync() -]
        public async Task<Product> SelectById(object id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            try
            {
                return await _context.Product.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Re-throw as a general Exception, hiding the implementation details.
                throw new Exception($"Could not retrieve product with ID '{id}' from the database.", ex);
            }
           
        }
        #endregion

        #region [- SaveChangesAsync() -]
        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }

        }
        #endregion

    }
}
