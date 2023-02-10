using AutoMapper;
using Repository.IRepository;
using Repository.Entity;
using Service.IService;
using Service.Dto;
using Service.Exceptions;
using Repository.Exceptions;
using Repository.Enums;

namespace Service.Service
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;

        public UsersService(IMapper mapper, IUsersRepository samplesRepository)
        {
            _mapper = mapper;
            _usersRepository = samplesRepository;
        }

        public async Task<RoleEnum?> CheckUser(string login, string password)
        {
            var user = (await _usersRepository.FindAsync(u => u.Login == login && u.Password == password))
                        .FirstOrDefault();

            if (user == null)
                return null;

            return user.Role;
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(UserRequestDto dto)
        {
            var entity = _mapper.Map<UserEntity>(dto);

            var created = await _usersRepository.AddAsync(entity);

            return _mapper.Map<UserDto>(created);
        }

        public async Task<UserDto> UpdateAsync(UserDto dto)
        {
            var entity = _mapper.Map<UserEntity>(dto);

            var updated = await _usersRepository.UpdateAsync(entity);

            return _mapper.Map<UserDto>(updated);
        }

        public async Task<UserDto> RemoveAsync(int id)
        {
            var entity = await _usersRepository.GetByIdAsync(id);
            if (entity == null)
                throw new ObjectNotFoundException($"User with id {id} not found.");

            var removed = await _usersRepository.RemoveAsync(entity);

            return _mapper.Map<UserDto>(removed);
        }
    }
}
