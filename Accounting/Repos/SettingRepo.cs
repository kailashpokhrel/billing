using Accounting.Interfaces;
using Accounting.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace Accounting.Repos
{
    public class SettingRepo : ISettingRepo
    {

        private readonly string _connectionString;

        public SettingRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<int> AddSettingAsync(Setting setting)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO Setting (Name, Value, Type, IsActive, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy) 
                    VALUES (@Name, @Value, @Type, @IsActive, @CreatedAt, @CreatedBy, @UpdatedAt, @UpdatedBy);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.QuerySingleAsync<int>(sql, setting);
        }

        public async Task<Setting> GetSettingByIdAsync(int Id)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Setting WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Setting>(sql, new { Id = Id });
        }

        public async Task<IEnumerable<Setting>> GetAllSettingAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Setting";
            return await connection.QueryAsync<Setting>(sql);
        }

        public async Task<IEnumerable<Setting>> GetPaymentSettingAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Setting where type='PaymentMethod'";
            return await connection.QueryAsync<Setting>(sql);
        }

        public async Task<Setting> GetTaxFromSettingAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Setting where Type='Tax'";
            return await connection.QueryFirstOrDefaultAsync<Setting>(sql);
        }

        public async Task<int> UpdateSettingAsync(Setting setting)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE Setting SET Name = @Name, Value = @Value, Type = @Type, 
                    IsActive = @IsActive, CreatedAt = @CreatedAt, CreatedBy = @CreatedBy, 
                    UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy
                    WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, setting);
        }

        public async Task<int> DeleteSettingAsync(int Id)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM Setting WHERE Id = @Id";
            return await connection.ExecuteAsync(sql, new { Id = Id });
        }


        public Task<IEnumerable<Setting>> GetSettingAsync()
        {
            throw new NotImplementedException();
        }
    }
}
