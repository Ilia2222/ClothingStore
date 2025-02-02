using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Repositories;
using ClothingStore.StaticData;

namespace ClothingStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Task.FromResult(StaticDb.Products);
        }

        public async Task AddAsync(Product product)
        {
            StaticDb.Products.Add(product);
            await Task.CompletedTask;
        }
    }
}