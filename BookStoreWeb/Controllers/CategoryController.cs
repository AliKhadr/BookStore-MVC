using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category) 
        {
            if (!String.IsNullOrEmpty(category.Name) && _db.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Category Name already exists");
            }

            if (ModelState.IsValid)
            {
                _db.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
