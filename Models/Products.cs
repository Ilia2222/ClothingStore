using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; }
    }
}
