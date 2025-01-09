using headhunter.Errors;
using System.Net;
using System.Text.Json;

namespace headhunter.Services
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<ApiExceptionMiddleware> _logger;

        public ApiExceptionMiddleware(RequestDelegate request, ILogger<ApiExceptionMiddleware> logger)
        {
            _request = request;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new ApiResponse((int)HttpStatusCode.InternalServerError, ex.StackTrace, ex.Message);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
