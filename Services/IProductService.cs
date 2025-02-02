using ClothingStore.Models;
namespace ClothingStore.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
}