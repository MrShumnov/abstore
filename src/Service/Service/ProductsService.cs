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
    public class ProductsService : IProductsService
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IMapper mapper, IProductsRepository samplesRepository)
        {
            _mapper = mapper;
            _productsRepository = samplesRepository;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productsRepository.GetByIdAsync(id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(ProductRequestDto dto)
        {
            var entity = _mapper.Map<ProductEntity>(dto);

            var created = await _productsRepository.AddAsync(entity);

            return _mapper.Map<ProductDto>(created);
        }

        public async Task<ProductDto> UpdateAsync(ProductDto dto)
        {
            var entity = _mapper.Map<ProductEntity>(dto);

            var updated = await _productsRepository.UpdateAsync(entity);

            return _mapper.Map<ProductDto>(updated);
        }

        public async Task<ProductDto> RemoveAsync(int id)
        {
            var entity = await _productsRepository.GetByIdAsync(id);
            if (entity == null)
                throw new ObjectNotFoundException($"Product with id {id} not found.");

            var removed = await _productsRepository.RemoveAsync(entity);

            return _mapper.Map<ProductDto>(removed);
        }
    }
}
