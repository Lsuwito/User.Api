using System;
using System.Net;

namespace User.Api.Exceptions
{
    public class InvalidRequestException : ApiException
    {
        public InvalidRequestException(string message, Exception innerException = null) : 
            base(HttpStatusCode.BadRequest, message, innerException) 
        {
        }
    }
}