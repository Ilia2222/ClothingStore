using ClothingStore.Models;
namespace ClothingStore.Repositories;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
}