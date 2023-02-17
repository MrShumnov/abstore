using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Exceptions;
using Service.IService;

namespace Controller.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : BaseController
    {
        private readonly ILogger<OrdersController> _logger;

        private readonly IOrdersService _ordersService;

        private readonly IProductsService _productsService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public OrdersController(ILogger<OrdersController> logger, IOrdersService ordersService, 
            IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _logger = logger;
            _ordersService = ordersService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IResult> Get()
        {
            var result = await _ordersService.GetAllAsync();

            return Results.Ok(result);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IResult> Post(OrderRequestDto dto)
        {
            var userId = GetUserId();
            if (_httpContextAccessor.HttpContext.User.IsInRole("User")
                && dto.UserId != userId)
                return Results.Forbid();

            var result = await _ordersService.CreateAsync(dto);

            return Results.Ok(_mapper.Map<OrderDto>(result));
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IResult> Patch(OrderDto dto)
        {
            var result = await _ordersService.UpdateAsync(dto);

            return Results.Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IResult> Get([FromRoute] int id)
        {
            var result = await _ordersService.GetByIdAsync(id);

            if (result == null)
                return Results.NotFound();

            return Results.Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResult> Delete([FromRoute] int id)
        {
            var result = await _ordersService.RemoveAsync(id);

            return Results.Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}/products")]
        public async Task<IResult> GetProducts([FromRoute] int id)
        {
            var result = await _ordersService.GetProductsAsync(id);

            return Results.Ok(result);
        }
    }
}