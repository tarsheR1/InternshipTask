using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace UserManagementService.PresentationLayer.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly RequestLoggingOptions _options;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger,
            IOptions<RequestLoggingOptions> options = null)
        {
            _next = next;
            _logger = logger;
            _options = options?.Value ?? new RequestLoggingOptions();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (ShouldSkipLogging(context))
            {
                await _next(context);
                return;
            }

            var startTime = DateTime.UtcNow;
            var stopwatch = Stopwatch.StartNew();

            // Логируем начало запроса
            LogRequestStart(context);

            try
            {
                await _next(context);
                stopwatch.Stop();

                // Логируем успешное завершение
                LogRequestSuccess(context, stopwatch.Elapsed);
            }
            catch (Exception)
            {
                stopwatch.Stop();
                // Логируем факт ошибки (детали обработает ExceptionMiddleware)
                LogRequestError(context, stopwatch.Elapsed);
                throw; // Пробрасываем исключение дальше
            }
        }

        private bool ShouldSkipLogging(HttpContext context)
        {
            return _options.ExcludedPaths?.Any(path =>
                context.Request.Path.StartsWithSegments(path)) == true;
        }

        private void LogRequestStart(HttpContext context)
        {
            if (_options.LogRequestStart)
            {
                _logger.LogInformation(
                    "HTTP {Method} {Path} started from {ClientIP}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Connection.RemoteIpAddress?.ToString());
            }
        }

        private void LogRequestSuccess(HttpContext context, TimeSpan elapsed)
        {
            var logLevel = GetLogLevel(context.Response.StatusCode);

            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["RequestId"] = context.TraceIdentifier,
                ["UserId"] = context.User.Identity?.Name ?? "Anonymous",
                ["ClientIP"] = context.Connection.RemoteIpAddress?.ToString(),
                ["UserAgent"] = context.Request.Headers.UserAgent.ToString()
            }))
            {
                _logger.Log(logLevel,
                    "HTTP {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    elapsed.TotalMilliseconds);
            }
        }

        private void LogRequestError(HttpContext context, TimeSpan elapsed)
        {
            _logger.LogError(
                "HTTP {Method} {Path} failed after {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                elapsed.TotalMilliseconds);
        }

        private static LogLevel GetLogLevel(int statusCode)
        {
            return statusCode switch
            {
                >= 500 => LogLevel.Error,
                >= 400 => LogLevel.Warning,
                _ => LogLevel.Information
            };
        }
    }

    public class RequestLoggingOptions
    {
        public bool LogRequestStart { get; set; } = false;
        public string[] ExcludedPaths { get; set; } = new[] { "/health", "/favicon.ico" };
    }
}
