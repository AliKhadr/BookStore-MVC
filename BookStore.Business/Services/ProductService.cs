using BookStore.Business.Services.IServices;
using Microsoft.EntityFrameworkCore;
using BookStore.DataAccess;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with id {id} not found.");
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

    }
}
