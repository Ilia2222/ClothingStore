using ClothingStore.Models;
namespace ClothingStore.Services;
using ClothingStore.Services;
using ClothingStore.Repositories;


public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    public ProductService(IProductRepository repo) { _repo = repo; }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _repo.GetAllAsync();
    public async Task AddAsync(Product product) => await _repo.AddAsync(product);
}