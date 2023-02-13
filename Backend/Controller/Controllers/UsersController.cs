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
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly IUsersService _usersService;

        private readonly IOrdersService _ordersService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IUsersService usersService, 
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IOrdersService ordersService)
        {
            _logger = logger;
            _usersService = usersService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _ordersService = ordersService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IResult> Get()
        {
            var result = await _usersService.GetAllAsync();

            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IResult> Post(UserRequestDto dto)
        {
            var result = await _usersService.CreateAsync(dto);

            return Results.Ok(_mapper.Map<UserDto>(result));
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch]
        public async Task<IResult> Patch(UserAdminDto dto)
        {
            var result = await _usersService.UpdateAsync(dto);

            return Results.Ok(result);
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        public async Task<IResult> Get([FromRoute] int id)
        {
            var result = await _usersService.GetByIdAsync(id);

            if (result == null)
                return Results.NotFound();

            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
                return Results.Ok(result);
            else
                return Results.Ok(_mapper.Map<UserDto>(result));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IResult> Delete([FromRoute] int id)
        {
            var result = await _usersService.RemoveAsync(id);

            return Results.Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}/orders")]
        public async Task<IResult> GetOrders(int id)
        {
            var result = await _ordersService.GetByUserIdAsync(id);

            return Results.Ok(result);
        }
    }
}