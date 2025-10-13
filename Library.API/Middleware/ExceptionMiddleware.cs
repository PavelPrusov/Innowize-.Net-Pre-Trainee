using Library.BusinessLogic.DTO;
using Library.BusinessLogic.Exceptions;
using System.Net;

namespace Library.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = exception switch
            {
                AppException appEx => HandleAppException(appEx),
                ArgumentException argEx => HandleArgumentException(argEx),
                _ => HandleGenericException(exception)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResponse.StatusCode;

            await context.Response.WriteAsJsonAsync(errorResponse);
        }

        private static ErrorResponceDto HandleAppException(AppException ex)
        {
            return new ErrorResponceDto(
                (int)ex.StatusCode,
                ex.Title,
                ex.Message
                );
        }

        private static ErrorResponceDto HandleArgumentException(ArgumentException ex)
        {
            return new ErrorResponceDto(
                (int)HttpStatusCode.BadRequest, 
                "Bad Request", 
                ex.Message 
                );
        }

        private static ErrorResponceDto HandleGenericException(Exception ex)
        {
            return new ErrorResponceDto(
                (int)HttpStatusCode.InternalServerError, 
                "Internal Server Error", 
                "An error occurred"
                );
          
        }
    }
}