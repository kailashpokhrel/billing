using Accounting.Models;

namespace Accounting.ViewModels
{
    public class LedgerVM
    {
        public int TransactionID { get; set; }               
        public int CustomerId { get; set; }    
        public string CustomerName { get; set; }
        public DateTime TransactionDate { get; set; }       
        public string NpTransactionDate { get; set; }              
        public string Particulars { get; set; }             
        public char DebitCredit { get; set; }                
        public decimal? DebitAmount { get; set; }            
        public decimal? CreditAmount { get; set; }          
    }

}
