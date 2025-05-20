using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Domain.Common;
using Products.Domain.Entities;
using Products.Infrastructure.Context;
using Products.Services.DTOs.Products;
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
                await _dbSet.AddAsync(product)
                    .ConfigureAwait(false);

                await _context.SaveChangesAsync()
                    .ConfigureAwait(false);

                return Result<Guid>.Success(product.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");

                return Result<Guid>.Failure("Error creating product");
            }
        }

        public async Task<Result<IEnumerable<Product>>> GetListAsync(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _dbSet
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToListAsync()
                    .ConfigureAwait(false);

                return Result<IEnumerable<Product>>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting list");

                return Result<IEnumerable<Product>>.Failure(ex.Message);
            }

        }
    }
}
