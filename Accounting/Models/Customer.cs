using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }  // Unique identifier for each customer

        [Required]
        [StringLength(100)]
        public string Name { get; set; }     // Name of the customer

        [Required]
        [Phone]
        [StringLength(15)]
        public string Mobile { get; set; }   // Mobile number of the customer

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }    // Email address of the customer

        [StringLength(255)]
        public string Address { get; set; }  // Address of the customer
    }
}
