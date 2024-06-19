using Accounting.Models;
using Accounting.ViewModels;

namespace Accounting.Interfaces
{
    public interface IExpenseEntryRepo
    {
        Task<IEnumerable<ExpenseEntryVM>> GetExpensesAsync();
        Task<ExpenseEntry> GetExpenseAsync(int id);
        Task<int> CreateExpenseAsync(ExpenseEntry expense);
        Task<int> UpdateExpenseAsync(ExpenseEntry expense);
        Task<int> DeleteExpenseAsync(int id);
    }
}
