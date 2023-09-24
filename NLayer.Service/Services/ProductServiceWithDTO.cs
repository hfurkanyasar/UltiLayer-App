using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductServiceWithDTO : ServiceWithDTO<Product, ProductDTO>, IProductServiceWithDTO
    {
        private readonly IProductRepository _repository;
        public ProductServiceWithDTO(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
        }

        public async Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductCreateDTO dto)
        {
            var newEntity = _mapper.Map<Product>(dto);

            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDTO = _mapper.Map<ProductDTO>(newEntity);
            return CustomResponseDTO<ProductDTO>.Succes(StatusCodes.Status200OK, newDTO);
        }

        public async Task<CustomResponseDTO<List<ProductDTO>>> AddRangeAsync(ProductCreateDTO dto)
        {
            var newEntities = _mapper.Map<List<Product>>(dto);

            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();

            var newDTOs = _mapper.Map<List<ProductDTO>>(newEntities);
            return CustomResponseDTO<List<ProductDTO>>.Succes(StatusCodes.Status200OK, newDTOs);
        }

        public async Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductsWithCategory()
        {
            var prod = await _repository.GetProductsWithCategory();
            var prodDTO = _mapper.Map<List<ProductWithCategoryDTO>>(prod);
            return CustomResponseDTO<List<ProductWithCategoryDTO>>.Succes(200, prodDTO);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductUpdateDTO dto)
        {
            var entites = _mapper.Map<Product>(dto);
            _repository.Update(entites);
            await _unitOfWork.CommitAsync();

            return CustomResponseDTO<NoContentDTO>.Succes(StatusCodes.Status204NoContent);
        }
        
    }
}
