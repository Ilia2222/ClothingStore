using ClothingStore.Models;

namespace ClothingStore.StaticData
{
    internal static class StaticDb
    {
        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Id = 1, Name = "Тениска", Price = 19.99M },
            new Product { Id = 2, Name = "Дънки", Price = 49.99M },
            new Product { Id = 3, Name = "Маратонки", Price = 89.99M }
        };
    }
}
