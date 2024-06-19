using Accounting.Interfaces;
using Accounting.Models;
using Accounting.ViewModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Accounting.Repos
{
    public class LedgerRepo : ILedgerRepo
    {
        private readonly string _connectionString;

        public LedgerRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(Ledger ledger)
        {
            const string sql = @"
            INSERT INTO Ledgers (TransactionDate, NpTransactionDate,CustomerId, Particulars, DebitCredit, DebitAmount, CreditAmount)
            VALUES (@TransactionDate, @NpTransactionDate,@CustomerId, @Particulars, @DebitCredit, @DebitAmount, @CreditAmount);
            SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QuerySingleAsync<int>(sql, ledger);
            }
        }

        public async Task<Ledger> GetByIdAsync(int transactionId)
        {
            const string sql = "SELECT * FROM Ledgers WHERE TransactionID = @TransactionID";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<Ledger>(sql, new { TransactionID = transactionId });
            }
        }

        public async Task<IEnumerable<LedgerVM>> GetLedgersByDateAsync(int customerId,string ledgerFromDate, string ledgerToDate)
        {
            const string query = @"
        SELECT L.*, C.Name as CustomerName 
        FROM Ledgers L 
        LEFT JOIN Customers C ON L.CustomerId = C.CustomerId 
        WHERE L.CustomerId = @CustomerId AND L.NpTransactionDate >= @ledgerFromDate AND L.NpTransactionDate <= @ledgerToDate";

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    return await connection.QueryAsync<LedgerVM>(query, new { customerId, ledgerFromDate, ledgerToDate });
                }
                catch (Exception ex)
                {
                    // Log the exception (you can use any logging framework or method here)
                    Console.Error.WriteLine($"Error retrieving ledgers by date: {ex.Message}");
                    throw;
                }
            }
        }


        public async Task<IEnumerable<Ledger>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Ledgers";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<Ledger>(sql);
            }
        }

        public async Task<int> UpdateAsync(Ledger ledger)
        {
            const string sql = @"
            UPDATE Ledgers
            SET CustomerId = @CustomerId,
                TransactionDate = @TransactionDate,
                NpTransactionDate = @NpTransactionDate,
                Particulars = @Particulars,
                DebitCredit = @DebitCredit,
                DebitAmount = @DebitAmount,
                CreditAmount = @CreditAmount
            WHERE TransactionID = @TransactionID";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, ledger);
            }
        }

        public async Task<int> DeleteAsync(int transactionId)
        {
            const string sql = "DELETE FROM Ledgers WHERE TransactionID = @TransactionID";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, new { TransactionID = transactionId });
            }
        }
    }
}
