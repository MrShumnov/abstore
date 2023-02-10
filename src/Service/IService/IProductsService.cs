using Service.Dto;

namespace Service.IService
{
    public interface IProductsService
    {
        Task<ProductDto?> GetByIdAsync(int id);

        Task<ProductDto> CreateAsync(ProductRequestDto dto);

        Task<ProductDto> UpdateAsync(ProductDto dto);

        Task<ProductDto> RemoveAsync(int id);
    }
}
