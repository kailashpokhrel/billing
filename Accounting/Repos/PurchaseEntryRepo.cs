using Accounting.Interfaces;
using Accounting.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Accounting.Repos
{
    public class PurchaseEntryRepo : IPurchaseEntryRepo
    {
        private readonly string _connectionString;

        public PurchaseEntryRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    
        public async Task<int> CreateAsync(PurchaseEntry purchase)
        {
            using var connection = CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var sqlPurchaseEntry = @"
                    INSERT INTO PurchaseEntries (BillNumber, VendorId, VendorName, BillDate, DueDate, PurchaseOrderNumber, VendorAddress, VendorContactPerson, VendorPhoneNumber, VendorEmail, TaxRate, TaxAmount, DiscountRate, DiscountAmount, SubTotal, TotalTax, TotalDiscount, ShippingCharges, OtherCharges, TotalAmount, PaymentTerms, PaymentMethod, PaymentStatus, PaymentDate, BankDetails, Attachment, Remarks, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, ProjectDepartment, ExpenseCategory, PANNumber) 
                    VALUES (@BillNumber, @VendorId,@VendorName, @BillDate, @DueDate, @PurchaseOrderNumber, @VendorAddress, @VendorContactPerson, @VendorPhoneNumber, @VendorEmail, @TaxRate, @TaxAmount, @DiscountRate, @DiscountAmount, @SubTotal, @TotalTax, @TotalDiscount, @ShippingCharges, @OtherCharges, @TotalAmount, @PaymentTerms, @PaymentMethod, @PaymentStatus, @PaymentDate, @BankDetails, NULL, @Remarks, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @ProjectDepartment, @ExpenseCategory, @PANNumber);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

                var billingId = await connection.QuerySingleAsync<int>(sqlPurchaseEntry, purchase, transaction);

                foreach (var item in purchase.PurchaseItems)
                {
                    item.BillID = billingId;
                }

                var sqlPurchaseItems = @"
                    INSERT INTO PurchaseItems (BillID, ItemDescription, ItemCode, Quantity, UnitPrice)
                    VALUES (@BillID, @ItemDescription, @ItemCode, @Quantity, @UnitPrice)";

                await connection.ExecuteAsync(sqlPurchaseItems, purchase.PurchaseItems, transaction);

                transaction.Commit();

                return billingId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw; 
            }
        }



        public async Task<int> DeleteAsync(int transactionId)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM PurchaseEntries WHERE BillID = @TransactionId";
            return await connection.ExecuteAsync(sql, new { TransactionId = transactionId });
        }

        public async Task<IEnumerable<PurchaseEntry>> GetAllAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM PurchaseEntries";
            return await connection.QueryAsync<PurchaseEntry>(sql);
        }

        public async Task<PurchaseEntry> GetByIdAsync(int transactionId)
        {
            using var connection = CreateConnection();
            var sql = @"
        SELECT pe.*, pi.*
        FROM PurchaseEntries pe
        LEFT JOIN PurchaseItems pi ON pe.BillID = pi.BillID
        WHERE pe.BillID = @TransactionId";
            var purchaseEntryDictionary = new Dictionary<int, PurchaseEntry>(); // Dictionary to track unique PurchaseEntry instances

            var result = await connection.QueryAsync<PurchaseEntry, PurchaseItem, PurchaseEntry>(
                sql,
                (purchaseEntry, purchaseItem) =>
                {
                    if (!purchaseEntryDictionary.TryGetValue(purchaseEntry.BillID, out var entry))
                    {
                        entry = purchaseEntry;
                        entry.PurchaseItems = new List<PurchaseItem>();
                        purchaseEntryDictionary.Add(entry.BillID, entry);
                    }

                    if (purchaseItem != null)
                    {
                        entry.PurchaseItems.Add(purchaseItem);
                    }

                    return entry;
                },
                new { TransactionId = transactionId },
                splitOn: "ItemID" 
            );
            return result.FirstOrDefault();

        }

        public async Task<int> UpdateAsync(PurchaseEntry purchase)
        {
            using var connection = CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                var sql = @"UPDATE PurchaseEntries 
                    SET BillNumber = @BillNumber, VendorId=@VendorId, VendorName = @VendorName, BillDate = @BillDate, DueDate = @DueDate, PurchaseOrderNumber = @PurchaseOrderNumber, 
                        VendorAddress = @VendorAddress, VendorContactPerson = @VendorContactPerson, VendorPhoneNumber = @VendorPhoneNumber, VendorEmail = @VendorEmail, 
                        TaxRate = @TaxRate, TaxAmount = @TaxAmount, DiscountRate = @DiscountRate, DiscountAmount = @DiscountAmount, SubTotal = @SubTotal, TotalTax = @TotalTax, 
                        TotalDiscount = @TotalDiscount, ShippingCharges = @ShippingCharges, OtherCharges = @OtherCharges, TotalAmount = @TotalAmount, PaymentTerms = @PaymentTerms, 
                        PaymentMethod = @PaymentMethod, PaymentStatus = @PaymentStatus, PaymentDate = @PaymentDate, BankDetails = @BankDetails,  
                        Remarks = @Remarks, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate, ProjectDepartment = @ProjectDepartment, 
                        ExpenseCategory = @ExpenseCategory , PANNumber=@PANNumber
                    WHERE BillID = @BillID";
                var billingId= await connection.ExecuteAsync(sql, purchase, transaction);
                foreach (var item in purchase.PurchaseItems)
                {
                    if (item.BillID == 0)
                    {
                        item.BillID = purchase.BillID;
                    }
                    if (item.ItemID == 0)
                    {
                        string insertSql = @"
                        INSERT INTO PurchaseItems (BillID, ItemDescription, ItemCode, Quantity, UnitPrice)
                        VALUES (@BillID, @ItemDescription, @ItemCode, @Quantity, @UnitPrice)";
                        await connection.ExecuteAsync(insertSql, item, transaction);
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
                        await connection.ExecuteAsync(updateSql, item, transaction);
                    }
                }
                transaction.Commit();

                return billingId;
            } catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
