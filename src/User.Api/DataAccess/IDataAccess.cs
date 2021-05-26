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
        /// <param name="command">A command definition</param>
        /// <typeparam name="T">Return value's type.</typeparam>
        /// <returns>A single value result from executing the command.</returns>
        Task<T> ExecuteScalarAsync<T>(CommandDefinition command);
    }
}