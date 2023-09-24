using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories.ToList());
            return CreateActionResult(CustomResponseDTO<List<CategoryDTO>>
                .Succes(200, categoriesDTO));
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetSingleCategoryByIDWithProductsAsync(int id)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIDWithProductsAsync(id));
        }

    }
}
