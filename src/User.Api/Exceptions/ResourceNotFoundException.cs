using System;
using System.Net;

namespace User.Api.Exceptions
{
    public class ResourceNotFoundException : ApiException
    {
        public ResourceNotFoundException(string message, Exception innerException = null) : 
            base(HttpStatusCode.NotFound, message, innerException) 
        {
        }
    }
}