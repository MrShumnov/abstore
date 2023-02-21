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
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersProductsRepository _ordersProductsRepository;

        public OrdersService(IMapper mapper, IOrdersRepository samplesRepository, 
            IProductsRepository productsRepository, IOrdersProductsRepository ordersProductsRepository)
        {
            _mapper = mapper;
            _ordersRepository = samplesRepository;
            _productsRepository = productsRepository;
            _ordersProductsRepository = ordersProductsRepository;
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _ordersRepository.GetByIdAsync(id);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateAsync(OrderRequestDto dto)
        {
            var entity = new OrderEntity { UserId = dto.UserId, Text = "", Price = 0 };
            var itemsUnique = dto.ItemsIds.Distinct();
            
            foreach (var itemId in dto.ItemsIds)
            {
                var product = await _productsRepository.GetByIdAsync(itemId);
                if (product == null)
                    throw new ObjectNotFoundException($"Product with id {itemId} not found");

                //orderProducts.Add(new OrderProductEntity { OrderId=})

                entity.Text += product.Symbol;
                entity.Price += product.Price;
            }

            var created = await _ordersRepository.AddAsync(entity);

            var orderProducts = new List<OrderProductEntity>();
            foreach (var itemId in dto.ItemsIds)
            {
                var orderProduct = orderProducts.Find(x => x.ProductId == itemId);

                if (orderProduct == null)
                    orderProducts.Add(new OrderProductEntity
                    {
                        OrderId = created.Id,
                        ProductId = itemId,
                        Qty = 1
                    });
                else
                    orderProduct.Qty += 1;
            }

            await _ordersProductsRepository.AddRangeAsync(orderProducts);

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
                throw new ObjectNotFoundException($"Order with id {id} not found.");

            var removed = await _ordersRepository.RemoveAsync(entity);

            return _mapper.Map<OrderDto>(removed);
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var entities = await _ordersRepository.GetAllAsync();

            return _mapper.Map<List<OrderDto>>(entities);
        }

        public async Task<List<OrderItemDto>> GetProductsAsync(int orderId)
        {
            var order = _ordersRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new ObjectNotFoundException($"Order with id {orderId} not found.");

            var orderProducts = await _ordersProductsRepository.FindAsync(x => x.OrderId == orderId);

            var items = new List<OrderItemDto>();

            foreach (var orderProduct in orderProducts)
            {
                var item = new OrderItemDto { Qty = orderProduct.Qty };

                item.Product = _mapper.Map<ProductDto>(
                    await _productsRepository.GetByIdAsync(orderProduct.ProductId));

                items.Add(item);
            }

            return items;
        }

        public async Task<List<OrderDto>> GetByUserIdAsync(int userId)
        {
            var entities = await _ordersRepository.FindAsync(x => x.UserId == userId);

            return _mapper.Map<List<OrderDto>>(entities);
        }
    }
}
