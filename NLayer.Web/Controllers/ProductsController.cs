using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    }
}
