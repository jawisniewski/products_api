using Products.Domain.Common;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public Task<Result<Guid>> CreateAsync(Product product);
        public Task<Result<IEnumerable<Product>>> GetListAsync(int pageNumber, int pageSize);
        public Task<Result<int>> GetCountAsync();
    }
}
