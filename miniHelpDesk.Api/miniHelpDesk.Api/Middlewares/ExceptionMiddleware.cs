using miniHelpDesk.Api.Contracts;
using miniHelpDesk.Application.common;
using System.Text.Json;

namespace miniHelpDesk.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                var payload = ApiResponse<object>.Fail(ex.Code, ex.Message);
                await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
            }
            catch
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var payload = ApiResponse<object>.Fail("internal_error", "Erro interno.");
                await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
            }
        }
    }
}
