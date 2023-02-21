using Service.Dto;
using Service.Dto.Filter;

namespace Service.IService
{
    public interface IProductsService
    {
        Task<List<ProductDto>> GetFilteredAsync(string? name, ProductTypeEnum[]? productTypes, ProductCaseEnum[]? productCases);

        Task<ProductDto?> GetByIdAsync(int id);

        Task<ProductDto> CreateAsync(ProductRequestDto dto);

        Task<ProductDto> UpdateAsync(ProductDto dto);

        Task<ProductDto> RemoveAsync(int id);
    }
}
