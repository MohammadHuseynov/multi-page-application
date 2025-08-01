using MultiPageApplication.ApplicationServices.Dtos;
using MultiPageApplication.ApplicationServices.Services.Contracts;
using MultiPageApplication.Models.DomainModels.ProductAggregates;
using MultiPageApplication.Models.Services.Contracts;


namespace MultiPageApplication.ApplicationServices.Services
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductRepository _productRepository;

        public ProductApplicationService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task PostProductDtoAsync(PostProductDto postProductDto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = postProductDto.Title,
                UnitPrice = postProductDto.UnitPrice,
                Quantity = postProductDto.Quantity
            };

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<GetProductDto> GetByIdProductAsync(Guid id)
        {
            var product = await _productRepository.SelectByIdAsync(id);
            if (product == null) return null;

            return new GetProductDto
            {
                Id = product.Id,
                Title = product.Title,
                UnitPrice = product.UnitPrice,
                Quantity = product.Quantity
            };
        }

        public async Task<List<GetProductDto>> GetAllProductAsync()
        {
            var products = await _productRepository.SelectAllAsync();
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

            return productDtos;
        }

        public async Task PutProductDtoAsync(PutProductDto putProductDto)
        {
            var product = await _productRepository.SelectByIdAsync(putProductDto.Id);
            if (product == null) return;

            product.Title = putProductDto.Title;
            product.UnitPrice = putProductDto.UnitPrice;
            product.Quantity = putProductDto.Quantity;

            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.SelectByIdAsync(id);
            if (product == null) return;

            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();
        }
    }
}
