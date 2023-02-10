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

        public async Task<UserAdminDto?> GetByIdAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);

            return _mapper.Map<UserAdminDto>(user);
        }

        public async Task<UserAdminDto> CreateAsync(UserRequestDto dto)
        {
            var entity = _mapper.Map<UserEntity>(dto);
            entity.Role = RoleEnum.User;

            var created = await _usersRepository.AddAsync(entity);

            return _mapper.Map<UserAdminDto>(created);
        }

        public async Task<UserAdminDto> UpdateAsync(UserAdminDto dto)
        {
            var entity = _mapper.Map<UserEntity>(dto);

            UserEntity updated;

            try
            {
                updated = await _usersRepository.UpdateAsync(entity);
            }
            catch (EntityNotExistsException)
            {
                throw new ObjectNotFoundException();
            }

            return _mapper.Map<UserAdminDto>(updated);
        }

        public async Task<UserAdminDto> RemoveAsync(int id)
        {
            var entity = await _usersRepository.GetByIdAsync(id);
            if (entity == null)
                throw new ObjectNotFoundException($"User with id {id} not found.");

            var removed = await _usersRepository.RemoveAsync(entity);

            return _mapper.Map<UserAdminDto>(removed);
        }

        public async Task<List<UserAdminDto>> GetAllAsync()
        {
            var entities = await _usersRepository.GetAllAsync();

            return _mapper.Map<List<UserAdminDto>>(entities);
        }
    }
}
