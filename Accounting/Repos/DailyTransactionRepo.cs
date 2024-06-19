using Accounting.Interfaces;
using Accounting.Models;
using Accounting.ViewModels;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace Accounting.Repos
{
    public class DailyTransactionRepo: IDailyTransactionRepo
    {
        private readonly string _connectionString;

        public DailyTransactionRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<DailyTransactionVM>> GetAllDailyTransactionsAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<DailyTransactionVM>("SELECT DT.*,BA.BankName FROM DailyTransactions DT LEFT JOIN BankAccounts BA ON DT.BankId=BA.Id ");
        }

        public async Task<DailyTransaction> GetDailyTransactionByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<DailyTransaction>("SELECT * FROM DailyTransactions WHERE Id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<DailyTransactionVM>> GetDailyTransactionByDateAsync(string transactionDate)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<DailyTransactionVM>("SELECT DT.*,BA.BankName FROM DailyTransactions DT LEFT JOIN BankAccounts BA ON DT.BankId=BA.Id WHERE  DT.NpTransactionDate = @NpTransactionDate", new { NpTransactionDate = transactionDate });
        }

        public async Task<IEnumerable<DailyTransactionVM>> GetDailyTransactionsByDateAsync(string transactionFromDate,string transactionToDate)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<DailyTransactionVM>("SELECT DT.*,BA.BankName FROM DailyTransactions DT LEFT JOIN BankAccounts BA ON DT.BankId=BA.Id WHERE  DT.NpTransactionDate >= @NpTransactionFromDate and DT.NpTransactionDate <= @NpTransactionToDate", new { NpTransactionFromDate = transactionFromDate, NpTransactionToDate = transactionToDate });
        }

        public async Task<int> InsertDailyTransactionAsync(DailyTransaction dailyTransaction)
        {
            using var connection = CreateConnection();
            string query = @"INSERT INTO DailyTransactions (TransactionDate,NpTransactionDate, Particular, InOut, BankId, Amount)
                         VALUES (@TransactionDate,@NpTransactionDate, @Particular, @InOut, @BankId, @Amount)";

            return await connection.ExecuteAsync(query, dailyTransaction);
        }

        public async Task<int> UpdateDailyTransactionAsync(DailyTransaction dailyTransaction)
        {
            using var connection = CreateConnection();
            string query = @"UPDATE DailyTransactions 
                         SET TransactionDate = @TransactionDate,
                             NpTransactionDate = @NpTransactionDate,
                             Particular = @Particular,
                             InOut = @InOut,
                             BankId = @BankId,
                             Amount = @Amount
                         WHERE Id = @Id";

            return await connection.ExecuteAsync(query, dailyTransaction);
        }

        public async Task<int> DeleteDailyTransactionAsync(int id)
        {
            using var connection = CreateConnection();
            string query = @"DELETE FROM DailyTransactions WHERE Id = @Id";

            return await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
