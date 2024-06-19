using Accounting.Models;

namespace Accounting.Interfaces
{
    public interface IBankAccountRepo
    {
        Task<IEnumerable<BankAccount>> GetBankAccountsAsync();
        Task<BankAccount> GetBankAccountAsync(int id);
        Task<int> CreateBankAccountAsync(BankAccount bankAccount);
        Task<int> UpdateBankAccountAsync(BankAccount bankAccount);
        Task<int> DeleteBankAccountAsync(int id);
    }
}
