using System;
using System.Text;
using System.Text.Json;
using User.Api.Models;

namespace User.Api.Services
{
    /// <inheritdoc />
    public class PaginationCursorConverter : IPaginationCursorConverter
    {
        /// <inheritdoc />
        public string ToString(PaginationCursor cursor) =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(cursor)));
        
        /// <inheritdoc />
        public PaginationCursor FromString(string cursorId)
        {
            if (string.IsNullOrWhiteSpace(cursorId))
            {
                throw new ArgumentNullException(nameof(cursorId));
            }

            var cursorBytes = Convert.FromBase64String(cursorId);
            return JsonSerializer.Deserialize<PaginationCursor>(Encoding.UTF8.GetString(cursorBytes));
        }
    }
}