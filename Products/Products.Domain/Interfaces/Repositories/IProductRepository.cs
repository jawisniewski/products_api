using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public Task<Guid> CreateAsync(Product product);
        public Task<IEnumerable<Product>> GetListAsync(int pageNumber, int pageSize);
        public Task<int> GetCountAsync();
    }
}
