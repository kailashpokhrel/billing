using Accounting.Interfaces;
using Accounting.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Accounting.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly string _connectionString;

        public CustomerRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(Customer customer)
        {
            const string sql = @"
                INSERT INTO Customers (Name, Mobile, Email, Address)
                VALUES (@Name, @Mobile, @Email, @Address);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QuerySingleAsync<int>(sql, customer);
            }
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            const string sql = "SELECT * FROM Customers WHERE CustomerId = @CustomerId";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { CustomerId = customerId });
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Customers";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<Customer>(sql);
            }
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            const string sql = @"
                UPDATE Customers
                SET Name = @Name,
                    Mobile = @Mobile,
                    Email = @Email,
                    Address = @Address
                WHERE CustomerId = @CustomerId";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, customer);
            }
        }

        public async Task<int> DeleteAsync(int customerId)
        {
            const string sql = "DELETE FROM Customers WHERE CustomerId = @CustomerId";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, new { CustomerId = customerId });
            }
        }
    }
}
