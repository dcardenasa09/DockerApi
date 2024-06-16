using System.Net;
using System.Text.Json;
using Shared.Lib.Models;
using Shared.Lib.Exceptions;

namespace Shared.Lib.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next) {
    
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context) {
        try {
            await _next(context);
        } catch (Exception error) {
            Dictionary<string, string[]> validationErrors = [];
            
            var response = context.Response;
            response.ContentType = "application/json";

            bool hasErrors = false;
            string title = "";
            switch(error) {
                case AppException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    hasErrors = true;
                    validationErrors = e.Errors;
                    break;
                case KeyNotFoundException e:
                    title = "The specific element was not found";
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException e:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            ErrorResponse errorResponse = new() {
                Code      = response.StatusCode,
                Title     = title,
                Message   = error.Message,
                Errors    = validationErrors,
                HasErrors = hasErrors
            };

            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }
}