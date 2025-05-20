using Microsoft.Extensions.DependencyInjection;
using Products.Services.Interfaces.Repositories;
using Products.Services.Interfaces.Services;
using Products.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
