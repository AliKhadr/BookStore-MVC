using BookStore.Business.Services;
using BookStore.Business.Services.IServices;
using BookStore.DataAccess;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert()
        {
            IEnumerable<SelectListItem> categoryList = (await _categoryService.GetAllCategoriesAsync()).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int id)
        {
            await _productService.DeleteProductAsync(id);
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

        #region
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync(true);
            return Json(new { data = products });
        }
        #endregion
    }
}
