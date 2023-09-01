using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Interview.Backend.Entities
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsVisible { get; set; }

        public bool IsMultiPurchase { get; set; }

        public List<Order> Orders { get; set; } = new();

    }
}
