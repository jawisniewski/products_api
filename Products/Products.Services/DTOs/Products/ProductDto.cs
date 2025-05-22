using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.DTOs.Products
{
    public class ProductDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}
