using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    public class PurchaseItem
    {
        [Key]
        public int ItemID { get; set; }
        public int BillID { get; set; }

        [Required(ErrorMessage = "Item Description is required")]
        [StringLength(255, ErrorMessage = "Item Description must be at most 255 characters")]
        public string? ItemDescription { get; set; }

        [StringLength(50, ErrorMessage = "Item Code must be at most 50 characters")]
        public string? ItemCode { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public PurchaseEntry? PurchaseEntry { get; set; }
    }
}
