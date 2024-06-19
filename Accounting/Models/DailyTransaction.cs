namespace Accounting.Models
{
    public class DailyTransaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }= DateTime.Now;
        public string NpTransactionDate { get; set; }
        public string? Particular { get; set; }
        public TransactionType InOut { get; set; }
        public PaymentType PaymentMode { get; set; }
        public int BankId { get; set; }
        public decimal Amount { get; set; }


    }

    public enum TransactionType
    {
        In=1,
        Out=2
    }
    public enum PaymentType
    {
        CA = 1,
        CH = 2
    }


}
