using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Domain.Entities;
using Products.Infrastructure.Context;
using Products.Domain.Extensions;
using Products.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductRepository> _logger;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<Product>();
        }

        public async Task<Guid> CreateAsync(Product product)
        {
            try
            {
                if (_dbSet.Any(x => x.Code.IsEqual(product.Code)))
                {
                    throw new InvalidOperationException("PRODUCT_CODE_ALREADY_EXISTS");
                }

                await _dbSet.AddAsync(product).ConfigureAwait(false);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return product.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetListAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _dbSet
                    .Skip(pageSize * pageNumber)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToListAsync()
                    .ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting list");
                throw;
            }
        }

        public async Task<int> GetCountAsync()
        {
            try
            {
                var result = await _dbSet.CountAsync().ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET_ERROR");
                throw;
            }
        }
    }
}
