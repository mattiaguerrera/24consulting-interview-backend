using System.ComponentModel.DataAnnotations;

namespace Interview.Backend.Entities
{
    public class OrderStepFlow
    {
        [Key]
        public int IdOrderStepFlow { get; set; }

        public OrderStepType IdStepPrev { get; set; }

        public OrderStepType IdStepNext { get; set; }
    }
}
