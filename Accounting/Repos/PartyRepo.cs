using Accounting.Interfaces;
using Accounting.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IO;

namespace Accounting.Repos
{
    public class PartyRepo : IPartyRepo
    {
        private readonly string _connectionString;

        public PartyRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<int> AddPartyAsync(Party party)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO Parties (Name, ContactName, Email, Phone, Address, Pan, IsVendor, IsCustomer) 
                    VALUES (@Name, @ContactName, @Email, @Phone, @Address, @Pan, @IsVendor, @IsCustomer);
                    SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.QuerySingleAsync<int>(sql, party);
        }

        public async Task<Party> GetPartyByIdAsync(int partyId)
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Parties WHERE PartyID = @PartyID";
            return await connection.QueryFirstOrDefaultAsync<Party>(sql, new { PartyID = partyId });
        }

        public async Task<IEnumerable<Party>> GetAllPartiesAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Parties";
            return await connection.QueryAsync<Party>(sql);
        }

        public async Task<IEnumerable<Party>> GetPartiesAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT * FROM Parties where IsVendor = 1";
            return await connection.QueryAsync<Party>(sql);
        }

        public async Task<int> UpdatePartyAsync(Party party)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE Parties SET Name = @Name, ContactName = @ContactName, 
                    Email = @Email, Phone = @Phone, Address = @Address, Pan = @Pan,
                    IsVendor = @IsVendor, IsCustomer = @IsCustomer
                    WHERE PartyID = @PartyID";
            return await connection.ExecuteAsync(sql, party);
        }

        public async Task<int> DeletePartyAsync(int partyId)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM Parties WHERE PartyID = @PartyID";
            return await connection.ExecuteAsync(sql, new { PartyID = partyId });
        }


    }

}
