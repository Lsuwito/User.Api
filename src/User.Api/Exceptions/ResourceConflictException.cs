using System;
using System.Net;
using User.Api.Models;

namespace User.Api.Exceptions
{
    public class ResourceConflictException : ApiException
    {
        public ResourceConflictException(string message, Exception innerException = null) : base(HttpStatusCode.Conflict, message, innerException) 
        {
        }
    }

    public class ApiException : Exception
    {
        public Error ErrorResponse { get; }

        public ApiException(HttpStatusCode code, string message, Exception innerException = null) : base(message, innerException)
        {
            ErrorResponse = new Error(code, message);
        }
    }
}