using Accounting.Interfaces;
using Accounting.Models;
using BlazorBootstrap;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Accounting.Repos
{
    public class ChartOfAccountRepo : IChartOfAccountRepo
    {
        private readonly string _connectionString;

        public ChartOfAccountRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        public async Task<int> CreateChartOfAccountAsync(ChartOfAccount bankAccount)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO ChartOfAccounts (AccountCode, AccountName, Description,AccountType) 
                        VALUES (@AccountCode, @AccountName, @Description,@AccountType);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.QuerySingleAsync<int>(sql, bankAccount);

        }

        public async Task<int> DeleteChartOfAccountAsync(int AccountID)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM ChartOfAccounts WHERE AccountID = @AccountID";
            return await connection.ExecuteAsync(sql, new { AccountID = AccountID });
        }

        public async Task<ChartOfAccount> GetChartOfAccountAsync(int AccountID)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM ChartOfAccounts WHERE AccountID = @AccountID";
            return await connection.QuerySingleOrDefaultAsync<ChartOfAccount>(sql, new { AccountID = AccountID });
        }

        public async Task<IEnumerable<ChartOfAccount>> GetChartOfAccountsAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM ChartOfAccounts";
            return await connection.QueryAsync<ChartOfAccount>(sql);
        }
        public async Task<IEnumerable<ChartOfAccount>> GetCategoryAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT AccountID,AccountName FROM ChartOfAccounts where AccountType='Expense'";
            return await connection.QueryAsync<ChartOfAccount>(sql);
        }

        public async Task<int> UpdateChartOfAccountAsync(ChartOfAccount bankAccount)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE ChartOfAccounts 
                        SET AccountCode = @AccountCode, AccountName = @AccountName, Description = @Description,AccountType = @AccountType,ParentAccountCode = @ParentAccountCode
                        WHERE AccountID = @AccountID";
            return await connection.ExecuteAsync(sql, bankAccount);
        }
    }
}
