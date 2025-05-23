using AutoMapper;
using Microsoft.Extensions.Logging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Services.Common;
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

        public async Task<Result> CreateAsync(ProductDto productDto)
        {
            try
            {
                var product = Product.Create(productDto.Name, productDto.Code, productDto.Price);

                var createProduct = await _productRepository.CreateAsync(product).ConfigureAwait(false);
                return Result<Guid>.Success(createProduct);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error creating product");
                return Result.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return Result.Failure("CREATING_ERROR");
            }
        }

        public async Task<Result<ProductGetListResponse>> GetListAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _productRepository.GetListAsync(pageNumber, pageSize).ConfigureAwait(false);
                var products = _mapper.Map<IEnumerable<ProductDto>>(result);
                var getProductsCount = await _productRepository.GetCountAsync().ConfigureAwait(false);

                var response = new ProductGetListResponse
                {
                    Total = getProductsCount,
                    Products = products
                };


                return Result<ProductGetListResponse>.Success(response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error getting list");
                return Result<ProductGetListResponse>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting list");
                return Result<ProductGetListResponse>.Failure("GET_LIST_ERROR");
            }
            
        }
    }
}
