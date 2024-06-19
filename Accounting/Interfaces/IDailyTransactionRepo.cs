using Accounting.Models;
using Accounting.ViewModels;

namespace Accounting.Interfaces
{
    public interface IDailyTransactionRepo
    {
        Task<IEnumerable<DailyTransactionVM>> GetAllDailyTransactionsAsync();
        Task<DailyTransaction> GetDailyTransactionByIdAsync(int id);
        Task<IEnumerable<DailyTransactionVM>> GetDailyTransactionByDateAsync(string transactionDate);
        Task<IEnumerable<DailyTransactionVM>> GetDailyTransactionsByDateAsync(string transactionFromDate, string transactionToDate);
        Task<int> InsertDailyTransactionAsync(DailyTransaction dailyTransaction);
        Task<int> UpdateDailyTransactionAsync(DailyTransaction dailyTransaction);
        Task<int> DeleteDailyTransactionAsync(int id);
    }
}
