using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.DTOs.Products
{
    public class ProductGetListResponse
    {
        public required IEnumerable<ProductDto> Products { get; set; }
        public int Total { get; set; }
    }
}
