using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace API;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string APIKEYNAME = "X-Api-Key";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 1. DAR PASE LIBRE A LA INTERFAZ WEB Y AL HISTORIAL EN EL NAVEGADOR
        var ruta = context.Request.Path.Value?.ToLower();
        if (string.IsNullOrEmpty(ruta) || 
            ruta == "/" || 
            ruta == "/index.html" || 
            ruta.StartsWith("/api/calidadagua/historial"))
        {
            await _next(context);
            return;
        }

        // 2. PARA OTRAS RUTAS (Como la evaluación POST), EXIGIR LA CONTRASEÑA
        var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
        var apiKeyValida = appSettings.GetValue<string>("ApiKey");

        if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Error: No se proporciono una API Key en la peticion.");
            return;
        }

        if (!apiKeyValida!.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Error: La API Key proporcionada es invalida.");
            return;
        }

        await _next(context);
    }
}
