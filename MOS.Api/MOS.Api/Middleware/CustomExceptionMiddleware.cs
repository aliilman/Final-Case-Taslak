using System.Diagnostics;
using System.Net;

using Newtonsoft.Json;
using Serilog;

namespace MOS.Middleware
{
  public class CustomExceptionMiddleware(RequestDelegate next)
  {
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
      var watch = Stopwatch.StartNew();

      try
      {
        string message = "[Request] Http " + context.Request.Method + " - " + context.Request.Path;
        Log.Information(message);

        await _next(context);
        watch.Stop();

        message = "[Response] Http " + context.Request.Method + " - " + context.Request.Path + " - Responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms.";
        Log.Information(message);
      }
      catch (Exception ex)
      {
        watch.Stop();
        await HandleExceptiona(context, ex, watch);
      }

    }

    private Task HandleExceptiona(HttpContext context, Exception ex, Stopwatch watch)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      Log.Error(ex, "UnexpectedError");
      Log.Fatal(
          $"Path={context.Request.Path} || " +
          $"Method={context.Request.Method} || " +
          $"Exception={ex.Message}" +
          $"Miliseconds={watch.Elapsed.TotalMilliseconds} ms"
      );

      var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

      return context.Response.WriteAsync(result);
    }
  }

  public static class CustomExceptionMiddlewareExtension
  {
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
  }
}