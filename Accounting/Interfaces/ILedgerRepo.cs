using Accounting.Models;
using Accounting.ViewModels;

namespace Accounting.Interfaces
{
    public interface ILedgerRepo
    {
        Task<int> CreateAsync(Ledger ledger);
        Task<Ledger> GetByIdAsync(int transactionId);
        Task<IEnumerable<Ledger>> GetAllAsync();
        Task<int> UpdateAsync(Ledger ledger);
        Task<int> DeleteAsync(int transactionId);
        Task<IEnumerable<LedgerVM>> GetLedgersByDateAsync(int CustomerId, string ledgerFromDate, string ledgerToDate);
    }
}
