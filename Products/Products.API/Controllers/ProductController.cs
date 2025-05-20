using Microsoft.AspNetCore.Mvc;
using Products.Domain.Common;
using Products.Services.DTOs.Products;
using Products.Services.Interfaces.Services;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet(Name = "GetList")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] ProductGetListRequest productGetListRequest)
        {
            var result = await _productService.GetListAsync(productGetListRequest.PageNumber, productGetListRequest.PageSize);
            
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost(Name = "Create")]
        public async Task<ActionResult> Create([FromBody] ProductDto product)
        {
            var createResult = await _productService.CreateAsync(product);

            if (createResult.IsFailure)
            {
                return BadRequest(createResult.Error);
            }

            return CreatedAtAction(nameof(Create), new { id = ((Result<Guid>)createResult).Value }, product);
        }
    }
}
