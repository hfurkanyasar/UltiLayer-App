using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productApiService.GetProductsWithCategoryAsync());
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categories, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO product)
        {

            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(product);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categories, "ID", "Name");
            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var prod = await _productApiService.GetByIDAsync(id);

            var categories = await _categoryApiService.GetAllAsync();

            ViewBag.categories = new SelectList(categories, "ID", "Name", prod.CategoryID);

            return View(prod);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO prodDTO)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(prodDTO);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categories, "ID", "Name", prodDTO.CategoryID);
            return View(prodDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
