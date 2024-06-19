using System.ComponentModel.DataAnnotations;

namespace Accounting.Models
{
    public class PurchaseEntry
    {
        [Key]
        public int BillID { get; set; }

        [Required(ErrorMessage = "BillNumber is required")]
        [StringLength(50, ErrorMessage = "BillNumber must be between 1 and 50 characters", MinimumLength = 1)]
        public string BillNumber { get; set; }

        [Required(ErrorMessage = "VendorName is required")]
        [StringLength(100, ErrorMessage = "VendorName must be between 1 and 100 characters", MinimumLength = 1)]
        public string VendorName { get; set; }

        public int? VendorID { get; set; }

        [Required(ErrorMessage = "BillDate is required")]
        [DataType(DataType.Date)]
        public DateTime BillDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [StringLength(50, ErrorMessage = "PurchaseOrderNumber must be at most 50 characters")]
        public string PurchaseOrderNumber { get; set; }

        [StringLength(255, ErrorMessage = "VendorAddress must be at most 255 characters")]
        public string VendorAddress { get; set; }

        [StringLength(100, ErrorMessage = "VendorContactPerson must be at most 100 characters")]
        public string VendorContactPerson { get; set; }

        [StringLength(50, ErrorMessage = "VendorPhoneNumber must be at most 50 characters")]
        public string VendorPhoneNumber { get; set; }

        [StringLength(100, ErrorMessage = "VendorEmail must be at most 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string VendorEmail { get; set; }

        [Range(0, 100, ErrorMessage = "TaxRate must be between 0 and 100")]
        public decimal? TaxRate { get; set; }

        public decimal? TaxAmount { get; set; }

        [Range(0, 100, ErrorMessage = "DiscountRate must be between 0 and 100")]
        public decimal? DiscountRate { get; set; }

        public decimal? DiscountAmount { get; set; }

        [Required(ErrorMessage = "SubTotal is required")]
        public decimal SubTotal { get; set; }

        public decimal? TotalTax { get; set; }

        public decimal? TotalDiscount { get; set; }

        public decimal? ShippingCharges { get; set; }

        public decimal? OtherCharges { get; set; }

        [Required(ErrorMessage = "TotalAmount is required")]
        public decimal TotalAmount { get; set; }

        [StringLength(100, ErrorMessage = "PaymentTerms must be at most 100 characters")]
        public string PaymentTerms { get; set; }

        [Required(ErrorMessage = "PaymentMethod is required")]
        [StringLength(50, ErrorMessage = "PaymentMethod must be at most 50 characters")]
        public string PaymentMethod { get; set; }

        [StringLength(50, ErrorMessage = "PaymentStatus must be at most 50 characters")]
        public string PaymentStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PaymentDate { get; set; }

        [StringLength(255, ErrorMessage = "BankDetails must be at most 255 characters")]
        public string BankDetails { get; set; }


        [StringLength(255, ErrorMessage = "Remarks must be at most 255 characters")]
        public string Remarks { get; set; }

        [StringLength(100, ErrorMessage = "CreatedBy must be at most 100 characters")]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }

        [StringLength(100, ErrorMessage = "ModifiedBy must be at most 100 characters")]
        public string ModifiedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(100, ErrorMessage = "ProjectDepartment must be at most 100 characters")]
        public string ProjectDepartment { get; set; }

        [Required(ErrorMessage = "ExpenseCategory is required")]

        [StringLength(100, ErrorMessage = "ExpenseCategory must be at most 100 characters")]
        public string ExpenseCategory { get; set; }

        public string PANNumber { get; set; }

        public List<PurchaseItem> PurchaseItems { get; set; }







    }
}
