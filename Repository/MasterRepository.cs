using Microsoft.Data.SqlClient;
using ISS_TEST.Models;
using System.Data;

namespace ISS_TEST.Repository
{
    public class MasterRepository : IMasterRepository
    {
        private readonly string _connectionString;

        public MasterRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection is missing.");
        }

        private SqlCommand CreateCommand(SqlConnection connection, string action)
        {
            var command = new SqlCommand("saveMaster", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@id", SqlDbType.BigInt).Value = DBNull.Value;
            command.Parameters.Add("@name", SqlDbType.NVarChar, -1).Value = DBNull.Value;
            command.Parameters.Add("@decription", SqlDbType.NVarChar, -1).Value = DBNull.Value;
            command.Parameters.Add("@isDelete", SqlDbType.Bit).Value = false;
            command.Parameters.Add("@action", SqlDbType.NVarChar, 20).Value = action;

            return command;
        }

        public async Task<List<MasterModel>> GetAllAsync()
        {
            var list = new List<MasterModel>();

            await using var connection = new SqlConnection(_connectionString);
            await using var command = CreateCommand(connection, "SELECT");

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(Map(reader));
            }

            return list;
        }

        public async Task<MasterModel?> GetByIdAsync(long id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = CreateCommand(connection, "SELECTBYID");

            command.Parameters["@id"].Value = id;

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            return await reader.ReadAsync() ? Map(reader) : null;
        }

        public async Task<bool> InsertAsync(MasterModel model)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = CreateCommand(connection, "INSERT");

            command.Parameters["@name"].Value = model.Name;
            command.Parameters["@decription"].Value = model.Description;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(MasterModel model)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = CreateCommand(connection, "UPDATE");

            command.Parameters["@id"].Value = model.Id;
            command.Parameters["@name"].Value = model.Name;
            command.Parameters["@decription"].Value = model.Description;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = CreateCommand(connection, "DELETE");

            command.Parameters["@id"].Value = id;
            command.Parameters["@isDelete"].Value = true;

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            return true;
        }

        private static MasterModel Map(SqlDataReader reader)
        {
            return new MasterModel
            {
                Id = Convert.ToInt64(reader["id"]),
                Name = reader["Name"]?.ToString() ?? string.Empty,
                Description = reader["Description"]?.ToString() ?? string.Empty
            };
        }
    }
}
