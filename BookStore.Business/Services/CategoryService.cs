using BookStore.Business.Services.IServices;
using Microsoft.EntityFrameworkCore;
using BookStore.DataAccess;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;
        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found.");
            }
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsCategoryNameUniqueAsync(string name, int? categoryId = null)
        {
            if (categoryId.HasValue)
            {
                return !await _db.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower() && c.Id != categoryId.Value);
            }
            else
            {
                return !await _db.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());
            }
        }
    }
}
