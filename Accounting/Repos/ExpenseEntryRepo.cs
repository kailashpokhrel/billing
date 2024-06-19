using Accounting.Interfaces;
using Accounting.Models;
using Accounting.ViewModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace Accounting.Repos
{
    public class ExpenseEntryRepo : IExpenseEntryRepo
    {
        private readonly string _connectionString;

        public ExpenseEntryRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        public async Task<int> CreateExpenseAsync(ExpenseEntry expense)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO ExpenseEntries (CustomerID, ExpenseDate, ExpenseCategory, ExpenseDescription, Amount, PaymentMethod, ReceiptAttachment, CreatedAt, UpdatedAt) 
                    VALUES (@CustomerID, @ExpenseDate, @ExpenseCategory, @ExpenseDescription, @Amount, @PaymentMethod, @ReceiptAttachment, @CreatedAt, @UpdatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.QuerySingleAsync<int>(sql, expense);
        }

        public async Task<int> DeleteExpenseAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM ExpenseEntries WHERE ExpenseID = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<ExpenseEntry> GetExpenseAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM ExpenseEntries WHERE ExpenseID = @Id";
            return await connection.QuerySingleOrDefaultAsync<ExpenseEntry>(sql, new { Id = id });

        }

        public async Task<IEnumerable<ExpenseEntryVM>> GetExpensesAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM ExpenseEntries";
            return await connection.QueryAsync<ExpenseEntryVM>(sql);
        }
      
        public async Task<int> UpdateExpenseAsync(ExpenseEntry expense)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE ExpenseEntries 
                    SET CustomerID = @CustomerID, ExpenseDate = @ExpenseDate, ExpenseCategory = @ExpenseCategory, 
                        ExpenseDescription = @ExpenseDescription, Amount = @Amount, PaymentMethod = @PaymentMethod, 
                        ReceiptAttachment = @ReceiptAttachment, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt
                    WHERE ExpenseID = @ExpenseID";
            return await connection.ExecuteAsync(sql, expense);
        }
    }
}
