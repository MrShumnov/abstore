using Repository.Enums;
using Service.Dto;

namespace Service.IService
{
    public interface IUsersService
    {
        Task<RoleEnum?> CheckUser(string login, string password);

        Task<UserDto?> GetByIdAsync(int id);

        Task<UserDto> CreateAsync(UserRequestDto dto);

        Task<UserDto> UpdateAsync(UserDto dto);

        Task<UserDto> RemoveAsync(int id);
    }
}
