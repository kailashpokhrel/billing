using Accounting.Interfaces;
using Accounting.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Accounting.Repos
{
    public class PurchaseItemRepo : IPurchaseItemRepo
    {
        private readonly string _connectionString;

        public PurchaseItemRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<int> CreateAsync(PurchaseItem purchaseItem)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO PurchaseItems (PurchaseID, ProductID, Quantity, UnitPrice, TotalPrice) 
                        VALUES (@PurchaseID, @ProductID, @Quantity, @UnitPrice, @TotalPrice);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.QuerySingleAsync<int>(sql, purchaseItem);
        }
        public async Task CreatePurchaseItemAsync(IEnumerable<PurchaseItem> purchaseItems)
        {
            using var connection = CreateConnection();
            string sql = @"
              INSERT INTO PurchaseItems (BillID, ItemDescription, ItemCode, Quantity, UnitPrice)
              VALUES (@BillID, @ItemDescription, @ItemCode, @Quantity, @UnitPrice)";


            foreach (var purchaseItem in purchaseItems)
            {
                await connection.ExecuteAsync(sql, purchaseItem);
            }
        }


        public async Task<int> DeleteAsync(int transactionId)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM PurchaseItems WHERE ItemID = @TransactionId";

            return await connection.ExecuteAsync(sql, new { TransactionId = transactionId });
        }

        public async Task<IEnumerable<PurchaseItem>> GetAllAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM PurchaseItems";
            return await connection.QueryAsync<PurchaseItem>(sql);
        }

        public async Task<PurchaseItem> GetByIdAsync(int transactionId)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM PurchaseItems WHERE ItemID = @TransactionId";
            return await connection.QueryFirstOrDefaultAsync<PurchaseItem>(sql, new { TransactionId = transactionId });
        }

        public async Task<int> UpdateAsync(PurchaseItem purchaseItem)
        {
            using var connection = CreateConnection();
            string sql = @"
                        UPDATE PurchaseItems 
                        SET ItemDescription = @ItemDescription, 
                            ItemCode = @ItemCode, 
                            Quantity = @Quantity, 
                            UnitPrice = @UnitPrice,
                            TotalPrice = @Quantity * @UnitPrice
                        WHERE BillID = @BillID AND ItemID = @ItemID";
            return await connection.ExecuteAsync(sql, purchaseItem);
        }

        public async Task UpdatePurchaseItemListAsync(List<PurchaseItem> purchaseItems)
        {
            using var connection = CreateConnection();
           
                try
                {
                    foreach (var purchaseItem in purchaseItems)
                    {
                        if (purchaseItem.ItemID == 0)
                        {
                            string insertSql = @"
                        INSERT INTO PurchaseItems (BillID, ItemDescription, ItemCode, Quantity, UnitPrice)
                        VALUES (@BillID, @ItemDescription, @ItemCode, @Quantity, @UnitPrice)";
                            await connection.ExecuteAsync(insertSql, purchaseItem);
                        }
                        else
                        {
                            string updateSql = @"
                        UPDATE PurchaseItems 
                        SET ItemDescription = @ItemDescription, 
                            ItemCode = @ItemCode, 
                            Quantity = @Quantity, 
                            UnitPrice = @UnitPrice
                        WHERE BillID = @BillID AND ItemID = @ItemID";
                            await connection.ExecuteAsync(updateSql, purchaseItem);
                        }
                    }
                 
                }
                catch
                {
                   
                    throw; 
                }
            
        }


    }

}

