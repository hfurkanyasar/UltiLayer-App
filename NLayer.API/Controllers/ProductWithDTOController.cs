using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithDTOController : CustomBaseController
    {
        private readonly IProductServiceWithDTO _productServiceWithDTO;

        public ProductWithDTOController(IProductServiceWithDTO productServiceWithDTO)
        {
            _productServiceWithDTO = productServiceWithDTO;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            //donus tipini productservicde ayarladım,bu yüzden tekrar dönüş 200 vermeme gerek yok 
            return CreateActionResult(await _productServiceWithDTO.GetProductsWithCategory());

        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _productServiceWithDTO.GetAllAsync());
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            return CreateActionResult(await _productServiceWithDTO.GetByIDAsync(id));

        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDTO productDto)
        {
            return CreateActionResult(await _productServiceWithDTO.AddAsync(productDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO productDto)
        {
           
            return CreateActionResult(await _productServiceWithDTO.UpdateAsync(productDto));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _productServiceWithDTO.RemoveAsync(id));
        }


        [HttpPost("SaveAll")]
        public async Task<IActionResult> SaveAll(List<ProductDTO> productDTOss)
        {
            return CreateActionResult(await _productServiceWithDTO.AddRangeAsync(productDTOss));
        }

        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> Remove(List<int> id)//
        {

            return CreateActionResult(await _productServiceWithDTO.RemoveRangeAsync(id));
        }
        [HttpDelete("Any/{id}")]
        public async Task<IActionResult> Any(int id)//
        {

            return CreateActionResult(await _productServiceWithDTO.AnyAsync(a=>a.ID==id));
        }
    }
}
