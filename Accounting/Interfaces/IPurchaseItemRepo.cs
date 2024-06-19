using Accounting.Models;

namespace Accounting.Interfaces
{
    public interface IPurchaseItemRepo
    {
        Task<int> CreateAsync(PurchaseItem purchaseitem);
        Task<PurchaseItem> GetByIdAsync(int transactionId);
        Task<IEnumerable<PurchaseItem>> GetAllAsync();
        Task<int> UpdateAsync(PurchaseItem purchaseitem);
        Task<int> DeleteAsync(int transactionId);
        Task CreatePurchaseItemAsync(IEnumerable<PurchaseItem> purchaseItems);
        Task UpdatePurchaseItemListAsync(List<PurchaseItem> purchaseItems);

    }
}
