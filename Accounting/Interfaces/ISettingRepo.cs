using Accounting.Models;

namespace Accounting.Interfaces
{
    public interface ISettingRepo
    {
        Task<int> AddSettingAsync(Setting settingId);
        Task<Setting> GetSettingByIdAsync(int settingId);
        Task<IEnumerable<Setting>> GetAllSettingAsync();
        Task<int> UpdateSettingAsync(Setting setting);
        Task<int> DeleteSettingAsync(int settingId);

        Task<IEnumerable<Setting>> GetSettingAsync();
        Task<Setting> GetTaxFromSettingAsync();
        Task<IEnumerable<Setting>> GetPaymentSettingAsync();


    }
}
