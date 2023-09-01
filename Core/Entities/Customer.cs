using System.ComponentModel.DataAnnotations;

namespace Interview.Backend.Entities
{    
    public class Customer
    {
        [Key]
        public int IdCustomer { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string CompanyVat { get; set; } = null!;

        public string CompanyName { get; set; } = null!;
    }
}
