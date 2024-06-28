using System.Net;
using System.Text.Json;
using Projekt.Errors;

namespace Projekt.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string errorType;
            string message = exception.Message;
            string details = exception.StackTrace;

            (statusCode, errorType) = exception switch
            {
                UnauthenticatedException _ => (HttpStatusCode.Unauthorized, "Unauthorized"),
                ResourceNotFoundException _ => (HttpStatusCode.NotFound, "Not Found"),
                EmptyResultException _ => (HttpStatusCode.NotFound, "Not Found"),
                BusinessRuleViolationException _ => (HttpStatusCode.Conflict, "Conflict"),
                DuplicatePurchaseException _ => (HttpStatusCode.BadRequest, "Bad Request"),
                InvalidInputException _ => (HttpStatusCode.BadRequest, "Invalid Input"),
                OperationFailedException _ => (HttpStatusCode.InternalServerError, "Operation Failed"),
                PermissionDeniedException _ => (HttpStatusCode.Forbidden, "Permission Denied"),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                Error = errorType,
                Message = message,
                Details = details
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }

        private class ErrorResponse
        {
            public string Error { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
        }
    }
}
