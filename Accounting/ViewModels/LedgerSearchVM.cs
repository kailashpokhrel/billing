using Accounting.Models;

namespace Accounting.ViewModels
{
    public class LedgerSearchVM
    {
        
        public int CustomerId { get; set; }
        public string NpLedgerFromDate { get; set; }
        public string NpLedgerToDate { get; set; }
        
    }

}
