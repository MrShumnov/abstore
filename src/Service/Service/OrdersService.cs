using AutoMapper;
using Repository.IRepository;
using Repository.Entity;
using Service.IService;
using Service.Dto;
using Service.Exceptions;
using Repository.Exceptions;
using Repository.Enums;
using Repository.Repository;

namespace Service.Service
{
    public class OrdersService : IOrdersService
    {
        private readonly IMapper _mapper;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(IMapper mapper, IOrdersRepository samplesRepository)
        {
            _mapper = mapper;
            _ordersRepository = samplesRepository;
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _ordersRepository.GetByIdAsync(id);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateAsync(OrderRequestDto dto)
        {
            var entity = _mapper.Map<OrderEntity>(dto);

            var created = await _ordersRepository.AddAsync(entity);

            return _mapper.Map<OrderDto>(created);
        }

        public async Task<OrderDto> UpdateAsync(OrderDto dto)
        {
            var entity = _mapper.Map<OrderEntity>(dto);

            var updated = await _ordersRepository.UpdateAsync(entity);

            return _mapper.Map<OrderDto>(updated);
        }

        public async Task<OrderDto> RemoveAsync(int id)
        {
            var entity = await _ordersRepository.GetByIdAsync(id);
            if (entity == null)
                throw new ObjectNotFoundException($"User with id {id} not found.");

            var removed = await _ordersRepository.RemoveAsync(entity);

            return _mapper.Map<OrderDto>(removed);
        }
    }
}
