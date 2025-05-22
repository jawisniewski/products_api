using Microsoft.AspNetCore.Mvc;
using Products.Domain.Common;
using Products.Services.DTOs.Products;
using Products.Services.Interfaces.Services;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult<ProductGetListResponse>> GetList([FromQuery] ProductGetListRequest productGetListRequest)
        {
            var result = await _productService.GetListAsync(productGetListRequest.PageIndex, productGetListRequest.PageSize);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("Create")]
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
