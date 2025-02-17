namespace CSharp.Advanced.Middlewares
{
    public class FactoryMiddleware(ILogger<FactoryMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            logger.LogInformation("Before request");

            await next(context);

            logger.LogInformation("After request");
        }
    }
}
