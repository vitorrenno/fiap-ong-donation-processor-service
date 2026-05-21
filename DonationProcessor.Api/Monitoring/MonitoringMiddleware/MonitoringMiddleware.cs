using System.Diagnostics;

namespace DonationProcessor.Api.Monitoring.MonitoringMiddleware
{
    public class MonitoringMiddleware
    {
        private readonly RequestDelegate _next;

        public MonitoringMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IMetricsService metricsService)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            string method = context.Request.Method;

            string endpoint = context.Request.Path.HasValue
                ? context.Request.Path.Value!
                : "unknown";

            int statusCode = context.Response.StatusCode;

            metricsService.ObserveRequest(
                method,
                endpoint,
                statusCode,
                stopwatch.Elapsed.TotalSeconds);
        }
    }
}
