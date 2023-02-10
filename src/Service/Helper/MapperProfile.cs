using AutoMapper;
using Repository.Entity;
using Service.Dto;

namespace Service.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<OrderEntity, OrderDto>().ReverseMap();
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<OrderProductEntity, OrderItemDto>().ReverseMap();

            CreateMap<UserEntity, UserRequestDto>().ReverseMap();
            CreateMap<OrderEntity, OrderRequestDto>().ReverseMap();
            CreateMap<ProductEntity, ProductRequestDto>().ReverseMap();
        }
    }
}
