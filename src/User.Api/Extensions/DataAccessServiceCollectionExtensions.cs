using Microsoft.Extensions.DependencyInjection;
using User.Api.DataAccess;

namespace User.Api.Extensions
{
    /// <summary>
    /// Provide extension methods for registering data access dependencies.
    /// </summary>
    public static class DataAccessServiceCollectionExtensions
    {
        /// <summary>
        /// Add Postgres data access dependencies to the container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the dependencies to.</param>
        public static void AddPostgresDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IDataAccess, NpgsqlDataAccess>();
            services.AddSingleton<ICommandProvider, NpgsqlCommandProvider>();
        }
    }
}