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
        Task<IResponse<bool>> Delete(DeleteProductDto deleteProductDto);

        // READ: return the read DTO wrapped in a response
        Task<IResponse<GetByIdProductDto>> GetByIdProductAsync(GetByIdProductDto getByIdProductDto);

        Task<IResponse<GetAllProductDto>> GetAllProductAsync();
    }
}
