using Microsoft.Extensions.DependencyInjection;
using Products.Services.Interfaces.Services;
using Products.Services.Services;

namespace Products.Services.DI
{
    public static class ServicesModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IProductService), typeof(ProductService));

            return services;
        }
    }
}
