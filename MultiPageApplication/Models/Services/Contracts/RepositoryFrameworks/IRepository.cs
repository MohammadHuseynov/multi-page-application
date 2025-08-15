namespace MultiPageApplication.Models.Services.Contracts.RepositoryFrameworks
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // CREATE
        Task Insert(TEntity entity);

        // READ
        Task<TEntity> SelectById(object id);

        Task<List<TEntity>> SelectAll();

        // UPDATE
        Task Update(TEntity entity);

        // DELETE
        Task Delete(TEntity entity);

        // SAVING
        Task SaveChangesAsync();
    }
}
