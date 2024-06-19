using Accounting.Models;

namespace Accounting.Interfaces
{
    public interface ICustomerRepo
    {
        Task<int> CreateAsync(Customer customer);
        Task<Customer> GetByIdAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<int> UpdateAsync(Customer customer);
        Task<int> DeleteAsync(int customerId);
    }
}
