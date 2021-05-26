using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using User.Api.Exceptions;

namespace User.Api.DataAccess
{
    /// <inheritdoc />
    public class PostgresDataAccess : IDataAccess
    {
        private readonly string _connectionString;
        private const string ConnectionStringKey = "UsersDb";
        private const string UniqueConstraintViolationCode = "23505";
        
        public PostgresDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(ConnectionStringKey);
        }

        /// <inheritdoc />
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
        
        /// <inheritdoc />
        public async Task<T> QuerySingleOrDefaultAsync<T>(CommandDefinition commandDefinition)
        {
            using var dbConnection = CreateConnection();
            try
            {
                return await dbConnection.QuerySingleOrDefaultAsync<T>(commandDefinition);
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