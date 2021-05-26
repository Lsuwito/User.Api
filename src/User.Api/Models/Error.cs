using System.Net;

namespace User.Api.Models
{
    public class Error
    {
        public Error(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }
        
        public HttpStatusCode Code { get; }
        public string Message { get; }
    }
}