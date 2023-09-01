using System;
using System.ComponentModel.DataAnnotations;

namespace Interview.Backend.Entities
{

    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

    }
}
    