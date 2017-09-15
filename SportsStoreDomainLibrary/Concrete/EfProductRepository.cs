using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportsStoreDomainLibrary.Abstract;
using SportsStoreDomainLibrary.Entities;
using System.Data.Entity;

namespace SportsStoreDomainLibrary.Concrete
{
    public class EfProductRepository : IProductRepository
    {
        private SportsStoreDbContext _context;
        public EfProductRepository()
        {
            _context = new SportsStoreDbContext();
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(int productId)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (prod != null)
            {
                _context.Products.Remove(prod);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            return prod;
        }

        public Task<List<Product>> GetProductsAsync()
        {
            //Cannot do a async and await as the return type is not IQueryable
            return _context.Products.ToListAsync();
        }

        public Task<List<Product>> GetProductsByCategoryAsync(string categoryName)
        {
            return _context.Products.Where(p => p.Category == categoryName).ToListAsync();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            //LOCAL: Gets an ObservableCollection<T> that represents a local view of all Added, Unchanged, and Modified entities in this set. This local view will stay in sync as entities are added or removed from the context. Likewise, entities added to or removed from the local view will automatically be added to or removed from the context.
            if (!_context.Products.Local.Any(p=>p.ProductId == product.ProductId))
            {
                _context.Products.Attach(product);
            }
            _context.Entry<Product>(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
