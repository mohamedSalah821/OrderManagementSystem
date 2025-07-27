using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrderManagementDbContext _context;

        public ProductRepository(OrderManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
