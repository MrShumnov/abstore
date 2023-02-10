using Service.Dto;

namespace Service.IService
{
    public interface IOrdersService
    {
        Task<OrderDto?> GetByIdAsync(int id);

        Task<OrderDto> CreateAsync(OrderRequestDto dto);

        Task<OrderDto> UpdateAsync(OrderDto dto);

        Task<OrderDto> RemoveAsync(int id);
    }
}
