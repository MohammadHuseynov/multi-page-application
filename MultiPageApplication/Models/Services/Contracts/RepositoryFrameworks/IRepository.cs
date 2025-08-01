
namespace MultiPageApplication.Models.Services.Contracts.RepositoryFrameworks
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // CREATE
        Task AddAsync(TEntity entity);

        // READ
        Task<TEntity> SelectByIdAsync(object id);

        Task<List<TEntity>> SelectAllAsync();

        // UPDATE
        Task UpdateAsync(TEntity entity);

        // DELETE
        Task DeleteAsync(TEntity entity);

        // PERSISTENCE
        Task SaveChangesAsync();
    }
}
