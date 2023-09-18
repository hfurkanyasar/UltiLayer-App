using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IGenericRepository<Product> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductWithCategoryDTO>> GetProductsWithCategory()
        {
            var prod = await _productRepository.GetProductsWithCategory();
            var prodDTO = _mapper.Map<List<ProductWithCategoryDTO>>(prod);
            return prodDTO;
        }
    }
}
