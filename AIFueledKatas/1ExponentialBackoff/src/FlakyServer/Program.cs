using FlakyServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new LoadGate(
    capacity: builder.Configuration.GetValue("LoadGate:Capacity", 300),
    window: TimeSpan.FromSeconds(builder.Configuration.GetValue("LoadGate:WindowSeconds", 1)),
    recoveryPeriod: TimeSpan.FromSeconds(builder.Configuration.GetValue("LoadGate:RecoverySeconds", 1))));
builder.Services.AddSingleton<RequestMetrics>();

var app = builder.Build();

app.MapGet("/work", (HttpContext context, LoadGate loadGate, RequestMetrics metrics) =>
{
    var now = DateTimeOffset.UtcNow;
    var admitted = loadGate.TryEnter(now);
    metrics.Record(now, admitted);

    if (!admitted)
    {
        context.Response.Headers.RetryAfter = Math.Ceiling(loadGate.RecoveryPeriod.TotalSeconds).ToString();
        return Results.Json(
            new { error = "Server overloaded. Retry later." },
            statusCode: StatusCodes.Status503ServiceUnavailable);
    }

    return Results.Ok(new { completedAt = now });
});

app.MapGet("/health", (LoadGate loadGate) => Results.Ok(new
{
    status = "running",
    overloaded = loadGate.IsOverloaded(DateTimeOffset.UtcNow),
    capacityPerWindow = loadGate.Capacity,
    windowMilliseconds = loadGate.Window.TotalMilliseconds
}));

app.MapGet("/metrics", (LoadGate loadGate, RequestMetrics metrics) => Results.Ok(new
{
    overloaded = loadGate.IsOverloaded(DateTimeOffset.UtcNow),
    loadGate.Capacity,
    windowMilliseconds = loadGate.Window.TotalMilliseconds,
    metrics = metrics.Snapshot()
}));

app.MapPost("/metrics/reset", (LoadGate loadGate, RequestMetrics metrics) =>
{
    loadGate.Reset();
    metrics.Reset();
    return Results.NoContent();
});

app.Run();