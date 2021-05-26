using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using User.Api.Exceptions;

namespace User.Api.DataAccess
{
    public class NpgsqlDataAccess : IDataAccess
    {
        private readonly string _connectionString;
        private const string ConnectionStringKey = "UsersDb";
        private const string UniqueConstraintViolationCode = "23505";
        
        
        public NpgsqlDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(ConnectionStringKey);
        }

        public async Task<T> ExecuteScalarAsync<T>(CommandDefinition commandDefinition)
        {
            using var dbConnection = CreateConnection();
            try
            {
                var result = await dbConnection.ExecuteScalarAsync<T>(commandDefinition);
                return result;
            }
            catch (PostgresException ex) when (ex.SqlState == UniqueConstraintViolationCode)
            {
                throw new UniqueConstraintViolationException(ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private IDbConnection CreateConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}