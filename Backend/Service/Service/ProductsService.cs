using AutoMapper;
using Repository.IRepository;
using Repository.Entity;
using Service.IService;
using Service.Dto;
using Service.Exceptions;
using System.Linq.Expressions;
using System.Collections.Generic;
using Repository.Exceptions;
using Repository.Repository;
using Service.Dto.Filter;
using System.Linq;

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
            ProductEntity updated;

            try
            {
                updated = await _productsRepository.UpdateAsync(entity);
            }
            catch (EntityNotExistsException)
            {
                throw new ObjectNotFoundException();
            }

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

        public async Task<List<ProductDto>> GetFilteredAsync(string? name, ProductTypeEnum[]? productTypes, ProductCaseEnum[]? productCases)
        {
            Func<ProductEntity, bool> lettersFilter = p => true;
            if (productTypes != null && productTypes.Length > 0)
            {
                if (productTypes.Contains(ProductTypeEnum.letters)
                    && !productTypes.Contains(ProductTypeEnum.other))
                    lettersFilter = p => char.IsLetter(p.Symbol);
                if (!productTypes.Contains(ProductTypeEnum.letters)
                    && productTypes.Contains(ProductTypeEnum.other))
                    lettersFilter = p => !char.IsLetter(p.Symbol);
            }

            Func<ProductEntity, bool> caseFilter = p => true;
            if (productCases != null && productCases.Length > 0)
            {
                if (productCases.Contains(ProductCaseEnum.lower)
                    && !productCases.Contains(ProductCaseEnum.upper))
                    caseFilter = p => !char.IsUpper(p.Symbol);
                if (!productCases.Contains(ProductCaseEnum.lower)
                    && productCases.Contains(ProductCaseEnum.upper))
                    caseFilter = p => char.IsUpper(p.Symbol) || !char.IsLetter(p.Symbol);
            }

            Expression<Func<ProductEntity, bool>> filter =
                p => ((name == null) || name.Contains(p.Symbol)) && 
                    lettersFilter(p) && caseFilter(p);

            // var entities = await _productsRepository.FindAsync(filter);
            var entities = await _productsRepository.GetAllAsync();
            entities = entities.Where(filter.Compile()).ToList();

            return _mapper.Map<List<ProductEntity>, List<ProductDto>>(entities);
        }
    }
}
