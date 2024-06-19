using Accounting.Models;

namespace Accounting.Interfaces
{
    public interface IChartOfAccountRepo
    {
        Task<IEnumerable<ChartOfAccount>> GetChartOfAccountsAsync();
        Task<ChartOfAccount> GetChartOfAccountAsync(int id);
        Task<int> CreateChartOfAccountAsync(ChartOfAccount bankAccount);
        Task<int> UpdateChartOfAccountAsync(ChartOfAccount bankAccount);
        Task<int> DeleteChartOfAccountAsync(int id);
        Task<IEnumerable<ChartOfAccount>> GetCategoryAsync();

    }
}
