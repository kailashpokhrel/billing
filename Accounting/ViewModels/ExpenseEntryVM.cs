using System.ComponentModel.DataAnnotations;

namespace Accounting.ViewModels
{
    public class ExpenseEntryVM
    {
     
        public int ExpenseID { get; set; }

   
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }

        public DateTime ExpenseDate { get; set; }

   
        public string? ExpenseCategory { get; set; }

 
        public string? ExpenseDescription { get; set; }

       
        public decimal Amount { get; set; }

      
        public string? PaymentMethod { get; set; }

        public byte[]? ReceiptAttachment { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
