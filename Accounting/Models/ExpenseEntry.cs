using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    public class ExpenseEntry
    {
        [Key]
        public int ExpenseID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        [StringLength(100)]
        public string? ExpenseCategory { get; set; }

        [StringLength(255)]
        public string? ExpenseDescription { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid amount format")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        public byte[]? ReceiptAttachment { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
  
    }
}
