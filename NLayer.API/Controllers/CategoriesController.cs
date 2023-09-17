using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
   
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[action]/{id}")]
        public async  Task<IActionResult> GetSingleCategoryByIDWithProductsAsync(int id)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIDWithProductsAsync(id));
        }
    }
}
