using Accounting.Models;

namespace Accounting.ViewModels
{
    public class DailyTransactionVM
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public string NpTransactionDate { get; set; }
        public string? Particular { get; set; }
        public TransactionType InOut { get; set; }
        public PaymentType PaymentMode { get; set; }
        public int BankId { get; set; }
        public decimal Amount { get; set; }
        public string BankName { get; set; }
    }

}
