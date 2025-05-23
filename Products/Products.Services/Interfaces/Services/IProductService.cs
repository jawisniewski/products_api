using Products.Services.Common;
using Products.Services.DTOs.Products;

namespace Products.Services.Interfaces.Services
{
    public interface IProductService
    {
        public Task<Result> CreateAsync(ProductDto product);
        public Task<Result<ProductGetListResponse>> GetListAsync(int pageNumber, int pageSize);
    }
}
