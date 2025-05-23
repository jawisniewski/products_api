using Products.Domain.Common;
using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public Task<Result<Guid>> CreateAsync(Product product);
        public Task<Result<IEnumerable<Product>>> GetListAsync(int pageNumber, int pageSize);
        public Task<Result<int>> GetCountAsync();
    }
}
