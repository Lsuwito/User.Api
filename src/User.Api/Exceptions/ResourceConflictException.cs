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
    
    public class ResourceNotFoundException : ApiException
    {
        public ResourceNotFoundException(string message, Exception innerException = null) : 
            base(HttpStatusCode.NotFound, message, innerException) 
        {
        }
    }
}