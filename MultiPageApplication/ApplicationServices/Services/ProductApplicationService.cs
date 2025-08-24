using MultiPageApplication.ApplicationServices.Dtos;
using MultiPageApplication.ApplicationServices.Services.Contracts;
using MultiPageApplication.Models.DomainModels.ProductAggregates;
using MultiPageApplication.Models.Services.Contracts;
using ResponseFramework;

namespace MultiPageApplication.ApplicationServices.Services
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductRepository _productRepository;

        public ProductApplicationService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        #region [- PostAsync() -]
        public async Task<IResponse<Guid>> Post(PostProductDto postProductDto)
        {
            // 1. VALIDATION: This is the most critical part. Never skip it.
            if (postProductDto == null)
                return new Response<Guid>("Request body cannot be null.");
            if (string.IsNullOrWhiteSpace(postProductDto.Title))
                return new Response<Guid>("Title is a required field.");
            if (postProductDto.UnitPrice < 0)
                return new Response<Guid>("Unit price cannot be negative.");

            // 2. MAPPING: Translate the DTO into the domain model.
            var product = new Product
            {
                Title = postProductDto.Title,
                UnitPrice = postProductDto.UnitPrice,
            };

            // 3. ORCHESTRATION: Command the repository to save the data.
            await _productRepository.Insert(product);

            return new Response<Guid>(product.Id);
        }
        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<bool>> Put(PutProductDto putProductDto)
        {
            // 1. VALIDATION
            if (putProductDto == null)
                return new Response<bool>("Request body cannot be null.");
            if (putProductDto.Id == Guid.Empty)
                return new Response<bool>("Product ID is required for an update.");
            if (string.IsNullOrWhiteSpace(putProductDto.Title))
                return new Response<bool>("Title is a required field.");
            if (putProductDto.UnitPrice < 0)
                return new Response<bool>("Unit price cannot be negative.");

            // 2. ORCHESTRATION: Retrieve the existing entity.
            var productResponse = await _productRepository.SelectById(putProductDto.Id);
            if (!productResponse.IsSuccessful || productResponse.Result == null)
                return new Response<bool>("Product not found to update.");
            

            // 3. MAPPING: Apply changes to the retrieved entity.
            var product = productResponse.Result;
            product.Title = putProductDto.Title;
            product.UnitPrice = putProductDto.UnitPrice;

            // 4. ORCHESTRATION: Command the repository to save.
            await _productRepository.Update(product);
            return new Response<bool>(true);
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<bool>> Delete(DeleteProductDto deleteProductDto)
        {
            // 1. VALIDATION
            if (deleteProductDto == null)
                return new Response<bool>("Request body cannot be null.");
            if (deleteProductDto.Id == Guid.Empty)
                return new Response<bool>("Product ID is required for deletion.");

            // 2. ORCHESTRATION: Ensure the product exists.
            var productResponse = await _productRepository.SelectById(deleteProductDto.Id);
            if (!productResponse.IsSuccessful || productResponse.Result == null)
                return new Response<bool>("Product not found to delete.");
            

            // 3. ORCHESTRATION: Command the repository.
            await _productRepository.Delete(productResponse.Result);
            return new Response<bool>(true);
        }
        #endregion


        #region [- GetByIdProductAsync() -]
        public async Task<IResponse<GetByIdProductDto>> GetByIdProductAsync(GetByIdProductDto getByIdProductDto)
        {
            // 1. VALIDATION
            if (getByIdProductDto == null)
                return new Response<GetByIdProductDto>("Request body cannot be null.");
            if (getByIdProductDto.Id == Guid.Empty)
                return new Response<GetByIdProductDto>("Product ID is required.");

            // 2. ORCHESTRATION
            var productResponse = await _productRepository.SelectById(getByIdProductDto.Id);
            if (!productResponse.IsSuccessful || productResponse.Result == null)
                return new Response<GetByIdProductDto>("Product not found.");
            

            // 3. MAPPING
            var product = productResponse.Result;
            var dto = new GetByIdProductDto
            {
                Id = product.Id,
                Title = product.Title,
                UnitPrice = product.UnitPrice,
            };

            return new Response<GetByIdProductDto>(dto);
        }
        #endregion

        #region [- GetAllProductAsync() -]
        public async Task<IResponse<GetAllProductDto>> GetAllProductAsync()
        {
            // 1. ORCHESTRATION
            var response = await _productRepository.SelectAll();
            if (!response.IsSuccessful || response.Result == null)
                return new Response<GetAllProductDto>(response.ErrorMessage ?? "Failed to retrieve products.");


            // 2. MAPPING: Use LINQ's 'Select' for a more concise mapping.
            var products = response.Result.Select(product => new GetByIdProductDto
            {
                Id = product.Id,
                Title = product.Title,
                UnitPrice = product.UnitPrice,
            }).ToList();

            var result = new GetAllProductDto
            {
                GetByIdProductDto = products
            };

            return new Response<GetAllProductDto>(result);
        }
        #endregion


        #region [- Private Mapping Methods -]
        //private Product ToProduct(PostProductDto dto)
        //{
        //    return new Product
        //    {
        //        Title = dto.Title,
        //        UnitPrice = dto.UnitPrice
        //    };
        //}

        //private GetByIdProductDto ToGetByIdProductDto(Product product)
        //{
        //    return new GetByIdProductDto
        //    {
        //        Id = product.Id,
        //        Title = product.Title,
        //        UnitPrice = product.UnitPrice
        //    };
        //}
        #endregion

    }
}