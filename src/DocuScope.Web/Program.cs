using DocuScope.Core;

var builder = WebApplication.CreateBuilder(args);

// 1. Register SearchEngine as a Singleton (one instance shared across all requests)
builder.Services.AddSingleton<SearchEngine>();

var app = builder.Build();

// 2. Enable serving static HTML/JS from wwwroot folder
app.UseDefaultFiles();
app.UseStaticFiles();

// 3. POST endpoint to index a URL
app.MapPost("/api/index", async (IndexRequest req, SearchEngine engine) =>
{
    if (string.IsNullOrWhiteSpace(req.Url))
    {
        return Results.BadRequest(new { message = "URL cannot be empty." });
    }

    bool success = await engine.IndexUrlAsync(req.Url);
    if (!success)
    {
        return Results.BadRequest(new { message = "Could not fetch or parse the URL." });
    }

    return Results.Ok(new { message = "Indexed successfully!" });
});

// 4. GET endpoint to search the index
app.MapGet("/api/search", (string keyword, SearchEngine engine) =>
{
    if (string.IsNullOrWhiteSpace(keyword))
    {
        return Results.BadRequest(new { message = "Keyword cannot be empty." });
    }

    List<string> results = engine.SearchIndex(keyword);
    return Results.Ok(new { count = results.Count, results });
});

app.Run();

// Data contract for the incoming POST JSON body
public record IndexRequest(string Url);