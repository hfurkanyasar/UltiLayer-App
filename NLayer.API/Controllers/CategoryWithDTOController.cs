using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryWithDTOController : CustomBaseController
    {
        private readonly IServiceWithDTO<Category, CategoryDTO> _serviceWithDTO;

        public CategoryWithDTOController(IServiceWithDTO<Category, CategoryDTO> serviceWithDTO)
        {
            _serviceWithDTO = serviceWithDTO;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _serviceWithDTO.GetAllAsync());
        }
       [ HttpPost]
        public async Task<IActionResult> Get(CategoryDTO category)
        {
            return CreateActionResult(await _serviceWithDTO.AddAsync(category));
        }
    }
}
