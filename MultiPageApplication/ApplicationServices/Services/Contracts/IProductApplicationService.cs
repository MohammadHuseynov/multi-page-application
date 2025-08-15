using MultiPageApplication.ApplicationServices.Dtos;
using ResponseFramework;

namespace MultiPageApplication.ApplicationServices.Services.Contracts
{
    public interface IProductApplicationService
    {
        // CREATE: return the new Product Id wrapped in a response
        Task<IResponse<Guid>> Post(PostProductDto postProductDto);

        // UPDATE: success/failure wrapped in a response
        Task<IResponse<bool>> Put(PutProductDto putProductDto);

        // DELETE: success/failure wrapped in a response
        Task<IResponse<bool>> Delete(Guid id);

        // READ: return the read DTO wrapped in a response
        Task<IResponse<GetProductDto>> GetByIdProductAsync(Guid id);

        Task<IResponse<List<GetProductDto>>> GetAllProductAsync();
    }
}
