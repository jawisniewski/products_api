using AutoMapper;
using Microsoft.Extensions.Logging;
using Products.Domain.Common;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Services.DTOs.Products;
using Products.Services.Interfaces.Services;

namespace Products.Services.Services
{
    public class ProductService : IProductService
    {
        public readonly ILogger<ProductService> _logger;
        public readonly IProductRepository _productRepository;
        public readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result> CreateAsync(ProductDto product)
        {
            var productResult = Product.Create(product.Name, product.Code, product.Price);

            if (productResult.IsFailure)
            {
                return productResult;
            }

            return await _productRepository.CreateAsync(productResult.Value).ConfigureAwait(false);
        }

        public async Task<Result<ProductGetListResponse>> GetListAsync(int pageNumber, int pageSize)
        {
            var result = await _productRepository.GetListAsync(pageNumber, pageSize).ConfigureAwait(false);

            if (result.IsFailure)            
                return Result<ProductGetListResponse>.Failure(result.ErrorCode);            

            var products =  _mapper.Map<IEnumerable<ProductDto>>(result.Value);
            
            var getProductsCount = await _productRepository.GetCountAsync().ConfigureAwait(false);

            if (getProductsCount.IsFailure)
            {
                return Result<ProductGetListResponse>.Failure(getProductsCount.ErrorCode);
            }

            var response = new ProductGetListResponse
            {
                Total = getProductsCount.Value,
                Products = products
            };

            return Result<ProductGetListResponse>.Success(response);
        }
    }
}
