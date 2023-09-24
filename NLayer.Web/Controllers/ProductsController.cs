using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _prodservices;
        private readonly ICategoryService _catservices;
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper, IProductService prodservices, ICategoryService catservices)
        {
            _catservices = catservices;
            _mapper = mapper;
            _prodservices = prodservices;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _prodservices.GetProductsWithCategory());
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _catservices.GetAllAsync();
            var catDTO = _mapper.Map<List<CategoryDTO>>(categories).ToList();
            ViewBag.categories = new SelectList(catDTO, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO product)
        {

            if (ModelState.IsValid)
            {
                await _prodservices.AddAsync(_mapper.Map<Product>(product));
                return RedirectToAction(nameof(Index));
            }
            var categories = await _catservices.GetAllAsync();
            var catDTO = _mapper.Map<List<CategoryDTO>>(categories).ToList(); ;
            ViewBag.categories = new SelectList(catDTO, "ID", "Name");
            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {

            var prod = await _prodservices.GetByIDAsync(id);

            var categories = await _catservices.GetAllAsync();
            var catDTO = _mapper.Map<List<CategoryDTO>>(categories).ToList(); ;
            ViewBag.categories = new SelectList(catDTO, "ID", "Name", prod.CategoryID);

            return View(_mapper.Map<ProductDTO>(prod));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO prodDTO)
        {
            if (ModelState.IsValid)
            {
                await _prodservices.UpdateAsync(_mapper.Map<Product>(prodDTO));
                return RedirectToAction(nameof(Index));
            }
            var categories = await _catservices.GetAllAsync();
            var catDTO = _mapper.Map<List<CategoryDTO>>(categories).ToList(); ;
            ViewBag.categories = new SelectList(catDTO, "ID", "Name", prodDTO.CategoryID);
            return View(prodDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            var prod = await _prodservices.GetByIDAsync(id);
            await _prodservices.RemoveAsync(prod);

            return RedirectToAction(nameof(Index));
        }

    }
}
