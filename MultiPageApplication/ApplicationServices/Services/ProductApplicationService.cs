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
        public async Task<IResponse<Guid>> Post(PostProductDto? postProductDto)
        {
            // Guard clauses
            if (postProductDto == null)
                return new Response<Guid>("Request body is required.");
        

            var product = new Product
            {
               
                Title = postProductDto.Title,
                UnitPrice = postProductDto.UnitPrice,

            };

            await _productRepository.Insert(product);
           

            return new Response<Guid>(product.Id);
        }
        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<bool>> Put(PutProductDto? putProductDto)
        {
            // Guard clauses
            if (putProductDto == null)
                return new Response<bool>("Request body is required.");
            if (putProductDto.Id == Guid.Empty)
                return new Response<bool>("Id is required.");


            var productResponse = await _productRepository.SelectById(putProductDto.Id);
            if (!productResponse.IsSuccessful || productResponse.Result == null)
            {
                return new Response<bool>("Product not found");
            }

            var product = productResponse.Result;
            product.Title = putProductDto.Title;
            product.UnitPrice = putProductDto.UnitPrice;


            await _productRepository.Update(product);
          

            return new Response<bool>(true);
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<bool>> Delete(DeleteProductDto? deleteProductDto)
        {
            // Guard clauses
            if (deleteProductDto == null)
                return new Response<bool>("Request body is required.");
            if (deleteProductDto.Id == Guid.Empty)
                return new Response<bool>("Id is required.");

            var productResponse = await _productRepository.SelectById(deleteProductDto.Id);
            if (!productResponse.IsSuccessful || productResponse.Result == null)
            {
                return new Response<bool>("Product not found");
            }

            await _productRepository.Delete(productResponse.Result);
          

            return new Response<bool>(true);
        }
        #endregion


        #region [- GetByIdProductAsync() -]
        public async Task<IResponse<GetByIdProductDto>> GetByIdProductAsync(GetByIdProductDto? getByIdProductDto)
        {
            // Guard clauses
            if (getByIdProductDto == null)
                return new Response<GetByIdProductDto>("Request body is required.");
            if (getByIdProductDto.Id == Guid.Empty)
                return new Response<GetByIdProductDto>("Id is required.");

            var productResponse = await _productRepository.SelectById(getByIdProductDto.Id);
            if (!productResponse.IsSuccessful || productResponse.Result == null)
            {
                return new Response<GetByIdProductDto>("Product not found");
            }

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
            var response = await _productRepository.SelectAll();
            if (!response.IsSuccessful || response.Result == null)
            {
                return new Response<GetAllProductDto>(response.ErrorMessage ?? "Failed to retrieve products.");
            }

            var products = new List<GetByIdProductDto>();
            foreach (var product in response.Result)
            {
                products.Add(new GetByIdProductDto
                {
                    Id = product.Id,
                    Title = product.Title,
                    UnitPrice = product.UnitPrice,

                });
            }

            var result = new GetAllProductDto
            {
                Products = products
            };

            return new Response<GetAllProductDto>(result);
        }
        #endregion
    }
}