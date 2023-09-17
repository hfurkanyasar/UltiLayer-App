using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper,IGenericRepository<Category> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async  Task<CustomResponseDTO<CategoryWithProductsDTO>> GetSingleCategoryByIDWithProductsAsync(int id)
        {
            var category = await _categoryRepo.GetSingleCategoryByIDWithProductsAsync(id);
            var categoryDto = _mapper.Map<CategoryWithProductsDTO>(category);
            return CustomResponseDTO<CategoryWithProductsDTO>.Succes(200, categoryDto);
        }
    }
}
