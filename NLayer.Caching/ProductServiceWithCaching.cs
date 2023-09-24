using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Caching
{

    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;
            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
            }
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expresion)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public Task<Product> GetByIDAsync(int id)
        {
            var prod = _memoryCache.Get<List<Product>>(CacheProductKey)
                .FirstOrDefault(a => a.ID == id);

            if (prod == null)
            {
                throw new NotFoundException($"{typeof(Product).Name} ({id}) not found");
            }
            return Task.FromResult(prod);
        }

        public Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory()
        {
            var prod = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var prodDTO = _mapper.Map<List<ProductWithCategoryDTO>>(prod);
            return Task.FromResult(CustomResponseDTO<List<ProductWithCategoryDTO>>.Succes(200,prodDTO));
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expresion)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expresion.Compile())
                .AsQueryable();
        }


        public async Task CacheAllProductAsync()
        {
            await _memoryCache.Set(CacheProductKey, _repository.GetAll().ToListAsync());

        }
    }
}
