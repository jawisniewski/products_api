namespace Products.API.Middlewars
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY_HEADER = "api-key";
        private readonly string _configuredApiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuredApiKey = configuration["ApiKey"]!;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey(APIKEY_HEADER))
            {
                await context.Response.WriteAsync("Unauthorized - Missing api-key header");
                context.Response.StatusCode = 401;

                return;
            }

            if (!context.Request.Headers.TryGetValue(APIKEY_HEADER, out var extractedApiKey) ||
                extractedApiKey != _configuredApiKey)
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync("Unauthorized - Invalid API Key");
                return;
            }

            await _next(context);
        }
    }
}
