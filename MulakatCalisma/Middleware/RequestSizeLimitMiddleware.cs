namespace MulakatCalisma.Middleware
{
    public class RequestSizeLimitMiddleware : IMiddleware
    {
        private readonly long _maxRequestSize;

        public RequestSizeLimitMiddleware(long maxRequestSize)
        {
            _maxRequestSize = maxRequestSize;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.ContentLength > _maxRequestSize)
            {
                context.Response.StatusCode = 413; // Payload Too Large
                await context.Response.WriteAsync("Request payload is too large");
            }
            else
            {
                await next(context);
            }
        }
    }
}
