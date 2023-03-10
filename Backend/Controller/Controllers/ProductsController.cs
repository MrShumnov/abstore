using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Dto.Filter;
using Service.Exceptions;
using Service.IService;

namespace Controller.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        private readonly ILogger<ProductsController> _logger;

        private readonly IProductsService _productsService;

        public ProductsController(ILogger<ProductsController> logger, IProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IResult> Get([FromQuery] string? name, [FromQuery(Name = "type")] ProductTypeEnum[] productTypes, [FromQuery(Name = "case")] ProductCaseEnum[] productCases)
        {
            var result = await _productsService.GetFilteredAsync(name, productTypes, productCases);

            return Results.Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IResult> Post(ProductRequestDto dto)
        {
            var result = await _productsService.CreateAsync(dto);

            return Results.Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IResult> Patch(ProductDto dto)
        {
            var result = await _productsService.UpdateAsync(dto);

            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IResult> Get([FromRoute] int id)
        {
            var result = await _productsService.GetByIdAsync(id);

            if (result == null)
                return Results.NotFound();

            return Results.Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResult> Delete([FromRoute] int id)
        {
            var result = await _productsService.RemoveAsync(id);

            return Results.Ok(result);
        }
    }
}