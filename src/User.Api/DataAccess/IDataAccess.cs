using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace User.Api.DataAccess
{
    /// <summary>
    /// Provide data access methods to the data store.
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Execute a data access command and returns a single value.  
        /// </summary>
        /// <param name="command">A command definition.</param>
        /// <typeparam name="T">Return value's type.</typeparam>
        /// <returns>A single value result from executing the command.</returns>
        Task<T> ExecuteScalarAsync<T>(CommandDefinition command);

        /// <summary>
        /// Query a single record. If entity is not found, return null.
        /// </summary>
        /// <param name="commandDefinition">A command definition.</param>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>An entity.</returns>
        Task<T> QuerySingleOrDefaultAsync<T>(CommandDefinition commandDefinition);

        /// <summary>
        /// Query records.
        /// </summary>
        /// <param name="commandDefinition">A command definition.</param>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>A list of entities.</returns>
        Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition commandDefinition);
    }
}