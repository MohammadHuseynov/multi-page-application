using MultiPageApplication.ApplicationServices.Dtos;

namespace MultiPageApplication.ApplicationServices.Services.Contracts
{
    public interface IProductApplicationService
    {
        // CREATE: A method that takes the create DTO.
        Task PostProductDtoAsync(PostProductDto postProductDto);

        // READ: Methods that return the read DTO.
        Task<GetProductDto> GetByIdProductAsync(Guid id); // Use Guid to match your DTOs

        Task<List<GetProductDto>> GetAllProductAsync();

        // UPDATE: A method that takes the update DTO.
        Task PutProductDtoAsync(PutProductDto putProductDto);

        // DELETE: A method that takes the Id.
        Task DeleteAsync(Guid id); // Use Guid
    }
}
