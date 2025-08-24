using Microsoft.EntityFrameworkCore;
using ResponseFramework;

namespace MultiPageApplication.Models.Services.Contracts.RepositoryFrameworks
{
    public interface IRepository<TEntity> where TEntity : class
    {

        #region [- CREATE -]
        // CREATE
        Task<IResponse<bool>> Insert(TEntity entity);
        #endregion

        #region [- READ -]
        // READ
        Task<IResponse<TEntity>> SelectById(Guid id);

        Task<IResponse<List<TEntity>>> SelectAll();
        #endregion


        #region [- UPDATE -]
        // UPDATE
        Task<IResponse<bool>> Update(TEntity entity);
        #endregion

        #region [- Delete -]
        // DELETE
        Task<IResponse<bool>> Delete(TEntity entity);
        #endregion


    }
}
