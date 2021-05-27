using System;
using System.Net;

namespace User.Api.Exceptions
{
    public class ResourceConflictException : ApiException
    {
        public ResourceConflictException(string message, Exception innerException = null) : 
            base(HttpStatusCode.Conflict, message, innerException) 
        {
        }
    }
}