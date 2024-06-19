using Accounting.Interfaces;
using Accounting.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Accounting.Repos
{
    public class BankAccountRepo : IBankAccountRepo
    {
        private readonly string _connectionString;

        public BankAccountRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM BankAccounts";
            return await connection.QueryAsync<BankAccount>(sql);
        }

        public async Task<BankAccount> GetBankAccountAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM BankAccounts WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<BankAccount>(sql, new { Id = id });
        }

        public async Task<int> CreateBankAccountAsync(BankAccount bankAccount)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO BankAccounts (BankName, AccountNo, Status) 
                        VALUES (@BankName, @AccountNo, @Status);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.QuerySingleAsync<int>(sql, bankAccount);
        }

        public async Task<int> UpdateBankAccountAsync(BankAccount bankAccount)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE BankAccounts 
                        SET BankName = @BankName, AccountNo = @AccountNo, Status = @Status 
                        WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, bankAccount);
        }

        public async Task<int> DeleteBankAccountAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM BankAccounts WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
