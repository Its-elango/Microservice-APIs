namespace Logistics_Management_System.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-Correlation-ID"))
            {
                var correlationId = Guid.NewGuid().ToString();
                context.Request.Headers.Add("X-Correlation-ID", correlationId);
                context.Response.Headers.Add("X-Correlation-ID", correlationId);
            }
            else
            {
                var correlationId = context.Request.Headers["X-Correlation-ID"].ToString();
                context.Response.Headers.Add("X-Correlation-ID", correlationId);
            }

            await _next(context);
        }
    }


    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}
