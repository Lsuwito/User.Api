using System;
using System.Net;
using User.Api.Models;

namespace User.Api.Exceptions
{
    public class ApiException : Exception
    {
        public Error ErrorResponse { get; }

        public ApiException(HttpStatusCode code, string message, Exception innerException = null) :
            base(message, innerException)
        {
            ErrorResponse = new Error(code, message);
        }
    }
}