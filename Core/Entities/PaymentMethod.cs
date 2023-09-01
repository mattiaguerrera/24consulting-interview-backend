using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview.Backend.Entities
{
    public class PaymentMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPaymentMethod { get; set; }

        public string? Description { get; set; }
    }

    public enum PaymentMethodType
    {
        Cash = 1,
        Visa,
        Mastercard,
        PayPal,
        BankTransfer
    }
}
