using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview.Backend.Entities
{
    public class OrderStep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdOrderStep { get; set; }

        public string? Description { get; set; }
    }

    public enum OrderStepType
    {
        Created = 1,
        WaitingPayment,
        Payed,
        InSpedition,
        Shipped,
        Delivered
    }

}
