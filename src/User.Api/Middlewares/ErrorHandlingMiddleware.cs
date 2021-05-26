using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;
using User.Api.Exceptions;
using User.Api.Models;

namespace User.Api.Middlewares
{
    /// <summary>
    /// Provide global error handling middleware capability.
    /// </summary>
    public class ApiErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ApiErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// This method handles exception by writing error json to the response and log the exception when necessary.
        /// This method will use the code in the ApiException as the http status code of the response.
        /// This method will generate an Internal Server Error http status code and generic error message
        /// for any unexpected exception, and the exception will be logged as well.
        /// </summary>
        /// <param name="context">A <see cref="HttpContext"/>.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException aex)
            {
                await WriteErrorResponse(context.Response, aex.ErrorResponse);
            }
            catch (Exception ex)
            {
                //unexpected exception
                _logger.Error(ex, "Unexpected exception occured");

                await WriteErrorResponse(context.Response, 
                    new Error(HttpStatusCode.InternalServerError, "Unexpected error."));
            }
        }

        private async Task WriteErrorResponse(HttpResponse response, Error error)
        {
            response.Clear();
            response.StatusCode = (int)error.Code;
            response.ContentType = "application/json; charset=UTF-8";

            await using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, error);
            var json = Encoding.UTF8.GetString(stream.ToArray());
            await response.WriteAsync(json);
            await response.Body.FlushAsync();
        }
    }
}