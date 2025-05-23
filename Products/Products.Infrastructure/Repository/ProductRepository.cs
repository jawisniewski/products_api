
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Domain.Common;
using Products.Domain.Entities;
using Products.Infrastructure.Context;
using Products.Domain.Extensions;
using Products.Domain.Interfaces.Repositories;

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

        public async Task<Result<Guid>> CreateAsync(Product product)
        {
            try
            {
                if(_dbSet.Any(x => x.Code.IsEqual(product.Code)))
                {
                    return Result<Guid>.Failure("PRODUCT_CODE_ALREADY_EXISTS");
                }
                await _dbSet.AddAsync(product)
                    .ConfigureAwait(false);

                await _context.SaveChangesAsync()
                    .ConfigureAwait(false);

                return Result<Guid>.Success(product.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");

                return Result<Guid>.Failure("CREATING_ERROR");
            }
        }

        public async Task<Result<IEnumerable<Product>>> GetListAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _dbSet
                    .Skip(pageSize * pageNumber)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToListAsync()
                    .ConfigureAwait(false);

                return Result<IEnumerable<Product>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting list");

                return Result<IEnumerable<Product>>.Failure("GET_ERROR");
            }

        }

        public async Task<Result<int>> GetCountAsync()
        {
            try
            {
                var result = await _dbSet.CountAsync()
                    .ConfigureAwait(false);

                return Result<int>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET_ERROR");
                return Result<int>.Failure("GET_ERROR");
            }
        }
    }
}
