using Repository.Enums;
using Service.Dto;

namespace Service.IService
{
    public interface IUsersService
    {
        Task<UserAdminDto?> CheckUser(string login, string password);

        Task<List<UserAdminDto>> GetAllAsync();

        Task<UserAdminDto?> GetByIdAsync(int id);

        Task<UserAdminDto> CreateAsync(UserRequestDto dto);

        Task<UserAdminDto> UpdateAsync(UserAdminDto dto);

        Task<UserAdminDto> RemoveAsync(int id);
    }
}
