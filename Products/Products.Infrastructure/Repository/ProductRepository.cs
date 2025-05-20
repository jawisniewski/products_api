using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Domain.Common;
using Products.Domain.Entities;
using Products.Infrastructure.Context;
using Products.Services.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly DbSet<Product> _dbSet;
        public ProductRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _dbSet = context.Set<Product>();
            _logger = logger;
        }

        public async Task<Result> CreateAsync(Product product)
        {
            try
            {
                await _dbSet.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return Result.Failure("Error creating product");
            }

            return Result.Success();
        }

        public async Task<Result<IEnumerable<Product>>> GetListAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _dbSet.Skip(pageSize * pageNumber).Take(pageSize).ToListAsync();
                return Result<IEnumerable<Product>>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Product>>.Failure(ex.Message);
            }
        }
    }
}
