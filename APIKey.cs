using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class APIKey
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;

    public APIKey(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apiKey = configuration["ApiKey"]; // Fetch API key from appsettings.json
    }

    public async Task Invoke(HttpContext context)
    {
        // Allow Swagger requests without API Key
        if (context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase))
        {
            await _next(context);
            return;
        }

        // Check if API Key is provided
        if (!context.Request.Headers.TryGetValue("API-Key", out var providedApiKey) || providedApiKey != _apiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Invalid API Key");
            return; 
        }

        await _next(context); // Continue request
    }
}
