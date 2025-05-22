using Products.Domain.Common;
using Products.Services.DTOs;
using Products.Services.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces.Services
{
    public interface IProductService
    {
        public Task<Result> CreateAsync(ProductDto product);
        public Task<Result<ProductGetListResponse>> GetListAsync(int pageNumber, int pageSize);
    }
}
