using System;
using System.ComponentModel.DataAnnotations;

namespace Interview.Backend.Entities
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }

        public int OrderStepId { get; set; }
        public OrderStep OrderStep { get; set; } = null!;

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = null!;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public List<Product> Products { get; set; } = new();

        public DateTime DateCreate { get; set; }

        public DateTime? DateEdit { get; set; }
    }
}
    