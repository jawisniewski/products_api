using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Interfaces.Repositories;
using Products.Infrastructure.Context;
using Products.Infrastructure.Repository;

namespace Products.Infrastructure.DI
{
    public static class InfrustructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseName)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName));

            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));

            return services;
        }
    }
}
