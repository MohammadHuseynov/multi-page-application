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
            try
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Title = postProductDto.Title,
                    UnitPrice = postProductDto.UnitPrice,
                    Quantity = postProductDto.Quantity
                };

                await _productRepository.Insert(product);
                await _productRepository.SaveChangesAsync();

                return new Response<Guid>(product.Id);
            }
            catch (Exception ex)
            {
                return new Response<Guid>(ex.Message);
            }
        }
        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<bool>> Put(PutProductDto putProductDto)
        {
            try
            {
                var product = await _productRepository.SelectById(putProductDto.Id);
                if (product == null)
                {
                    return new Response<bool>("Product not found");
                }

                product.Title = putProductDto.Title;
                product.UnitPrice = putProductDto.UnitPrice;
                product.Quantity = putProductDto.Quantity;

                await _productRepository.Update(product);
                await _productRepository.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(ex.Message);
            }
        }
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<bool>> Delete(Guid id)
        {
            try
            {
                var product = await _productRepository.SelectById(id);
                if (product == null)
                {
                    return new Response<bool>("Product not found");
                }

                await _productRepository.Delete(product);
                await _productRepository.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(ex.Message);
            }
        }
        #endregion


        #region [- GetByIdProductAsync() -]
        public async Task<IResponse<GetProductDto>> GetByIdProductAsync(Guid id)
        {
            try
            {
                var product = await _productRepository.SelectById(id);
                if (product == null)
                {
                    return new Response<GetProductDto>("Product not found");
                }

                var dto = new GetProductDto
                {
                    Id = product.Id,
                    Title = product.Title,
                    UnitPrice = product.UnitPrice,
                    Quantity = product.Quantity
                };

                return new Response<GetProductDto>(dto);
            }
            catch (Exception ex)
            {
                return new Response<GetProductDto>(ex.Message);
            }
        }
        #endregion

        #region [- GetAllProductAsync() -]
        public async Task<IResponse<List<GetProductDto>>> GetAllProductAsync()
        {
            try
            {
                var products = await _productRepository.SelectAll();
                var productDtos = new List<GetProductDto>();

                foreach (var product in products)
                {
                    productDtos.Add(new GetProductDto
                    {
                        Id = product.Id,
                        Title = product.Title,
                        UnitPrice = product.UnitPrice,
                        Quantity = product.Quantity
                    });
                }

                return new Response<List<GetProductDto>>(productDtos);
            }
            catch (Exception ex)
            {
                return new Response<List<GetProductDto>>(ex.Message);
            }
        }
        #endregion
    }
}
