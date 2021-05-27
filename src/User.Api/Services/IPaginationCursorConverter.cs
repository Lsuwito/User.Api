using User.Api.Models;

namespace User.Api.Services
{
    /// <summary>
    /// Provide methods to convert pagination cursor from or to string.
    /// </summary>
    public interface IPaginationCursorConverter
    {
        /// <summary>
        /// Convert pagination cursor object to Base64 string
        /// </summary>
        /// <param name="cursor">An instance of <see cref="PaginationCursor"/>.</param>
        /// <returns>Base64 string</returns>
        string ToString(PaginationCursor cursor);

        /// <summary>
        /// Convert Base64 string to PaginationCursor object
        /// </summary>
        /// <param name="cursorId">A Base64 string</param>
        /// <returns>An instance of <see cref="PaginationCursor"/>.</returns>
        PaginationCursor FromString(string cursorId);
    }
}