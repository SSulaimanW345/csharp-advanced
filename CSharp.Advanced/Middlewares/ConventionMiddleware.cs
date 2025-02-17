namespace CSharp.Advanced.Middlewares
{
    public class ConventionMiddleware(
     RequestDelegate next,
     ILogger<ConventionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            logger.LogInformation("Before request");

            await next(context);

            logger.LogInformation("After request");
        }
    }
}
