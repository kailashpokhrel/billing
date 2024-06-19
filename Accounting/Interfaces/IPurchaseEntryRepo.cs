using Accounting.Models;
using Accounting.ViewModels;

namespace Accounting.Interfaces
{
    public interface IPurchaseEntryRepo
    {
        Task<int> CreateAsync(PurchaseEntry purchase);
        Task<PurchaseEntry> GetByIdAsync(int transactionId);
        Task<IEnumerable<PurchaseEntry>> GetAllAsync();
        Task<int> UpdateAsync(PurchaseEntry ledger);
        Task<int> DeleteAsync(int transactionId);

    }
}
