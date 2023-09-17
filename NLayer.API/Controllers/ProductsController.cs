using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;

        private readonly IProductService _service;

        public ProductsController(IMapper mapper,IProductService service)
        {
            _mapper = mapper;

           _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            //donus tipini productservicde ayarladım,bu yüzden tekrar dönüş 200 vermeme gerek yok 
            return CreateActionResult(await _service.GetProductsWithCategory());

        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var product = await _service.GetAllAsync();
            var productDTOs = _mapper.Map<List<ProductDTO>>(product.ToList());

            return CreateActionResult(CustomResponseDTO<List<ProductDTO>>.Succes(200, productDTOs));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var product = await _service.GetByIDAsync(id);
            var productDTOs = _mapper.Map<ProductDTO>(product);

            return CreateActionResult(CustomResponseDTO<ProductDTO>.Succes(200, productDTOs));

        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productDTOs = _mapper.Map<ProductDTO>(product);

            return CreateActionResult(CustomResponseDTO<ProductDTO>.Succes(201, productDTOs));

        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Succes(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var prod = await _service.GetByIDAsync(id);
            await _service.RemoveAsync(prod);
            // idye göre product var mı yok kontrolleri
            // exeption katmanı yazılacak if bloklarından kurtulup yazılcak.

            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Succes(204));

        }
    }
}
