namespace Accounting.Models
{
    public class Ledger
    {
        public int TransactionID { get; set; }               // Unique identifier for each transaction
        public int CustomerId { get; set; }               // Unique identifier for each transaction
        public DateTime TransactionDate { get; set; }        // Date of the transaction
        public string NpTransactionDate { get; set; }              // Nepali Date of the transaction
        public string Particulars { get; set; }              // Description of the transaction
        public char DebitCredit { get; set; }                // Indicator for Debit (D) or Credit (C)
        public decimal? DebitAmount { get; set; }            // Amount debited (if applicable)
        public decimal? CreditAmount { get; set; }           // Amount credited (if applicable)
    }
}
