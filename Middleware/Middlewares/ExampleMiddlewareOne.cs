using System.Globalization;

namespace Middleware.Middlewares
{
    public class ExampleMiddlewareOne
    {
        private readonly RequestDelegate _next;

        public ExampleMiddlewareOne(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync(
               $"CurrentCulture.DisplayName 3: {CultureInfo.CurrentCulture.DisplayName}");

            await _next(context);
        }
    }
}
