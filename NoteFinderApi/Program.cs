//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using NoteFinder.Interfaces;
using NoteFinder.Service;
using NoteFinder.Service.Definitions;
using NoteFinder.Helpers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net.Http.Headers;
using NoteFinder.ExternalInfo.Service.Data;
using NoteFinder.ExternalInfo.Service;
using NoteFinder.Service.Definitions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddHttpClient();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddLogging();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "NoteFinder API",
        Version = "v1",
        Description = "An API for music theory operations including scales, chords, and insights."
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    Console.WriteLine($"XML Path: {Path.GetFullPath(xmlPath)}"); // Debug line!
    c.IncludeXmlComments(xmlPath);
});


builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NoteFinder API V1");
    c.RoutePrefix = string.Empty;
});




app.UseCors("AllowAll");


/// <summary>
/// Retrieves the notes of a given scale in a specific key.
/// </summary>
/// <param name="key">The root note of the scale (e.g., C, F#, Bb)</param>
/// <param name="scaleName">The name of the scale (e.g., Major, Minor, Dorian)</param>
/// <returns>A collection of notes in the specified scale</returns>
/// <response code="200">Returns the collection of notes in the scale</response>
/// <response code="400">If the key or scale name is invalid</response>
app.MapGet("/api/scale/notes", ([FromQuery] string key, [FromQuery] string scaleName) =>
{
    try
    {
        IInterval[] scaleIntervals = DefinitionsHelper.GetScaleIntervals(scaleName);
        NoteCollection noteCollection = new NoteCollection(key.ToUpper(), scaleIntervals);
       // noteCollection.SetProperlyNamedNotes();

        return Results.Ok(noteCollection);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Error: {ex.Message}");
    }
})
.WithName("GetScaleNotes")
.Produces<NoteCollection>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

/// <summary>
/// Retrieves the chord of a specific scale degree in a given key and scale.
/// </summary>
/// <param name="key">The root note of the scale (e.g., C, F#, Bb)</param>
/// <param name="scaleName">The name of the scale (e.g., Major, Minor, Dorian)</param>
/// <param name="degree">The scale degree (1-7) to get the chord for</param>
/// <returns>Information about the chord, including its root, type, and notes</returns>
/// <response code="200">Returns the chord information</response>
/// <response code="400">If the parameters are invalid</response>
app.MapGet("/api/chord-of-scale-degree", (string key, string scaleName, int degree) =>
{
    try
    {
        var (chord, chordName) = ChordDefinitions.GetChordOfScaleDegree(key, scaleName, degree);
        var chordRoot = chord.NotesAndIntervals[0].Note.Note;

        // Get the Roman numeral representation
        string romanNumeral = ChordDefinitions.GetRomanNumeral(scaleName, degree, chordName);

        return Results.Ok(new
        {
            Key = key,
            Scale = scaleName,
            Degree = degree,
            ChordRoot = chordRoot,
            ChordType = chordName,
            RomanNumeral = romanNumeral,
            ChordNotes = chord.GetProperlyNamedNotes()
        });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("GetChordOfScaleDegree")
.Produces<object>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

/// <summary>
/// Retrieves the notes of a specific chord in a given key.
/// </summary>
/// <param name="key">The root note of the chord (e.g., C, F#, Bb)</param>
/// <param name="chordName">The name of the chord (e.g., Major, Minor, Dominant7)</param>
/// <returns>A collection of notes in the specified chord</returns>
/// <response code="200">Returns the collection of notes in the chord</response>
/// <response code="400">If the key or chord name is invalid</response>
app.MapGet("/api/chord/notes", ([FromQuery] string key, [FromQuery] string chordName) =>
{
    try
    {
        IInterval[] chordIntervals = DefinitionsHelper.GetChordIntervals(chordName);
        NoteCollection noteCollection = new NoteCollection(key.ToUpper(), chordIntervals);
       // noteCollection.SetProperlyNamedNotes();

        return Results.Ok(noteCollection);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Error: {ex.Message}");
    }
})
.WithName("GetChordNotes")
.Produces<NoteCollection>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

/// <summary>
/// Retrieves musical insights about a specific scale in a given key.
/// </summary>
/// <param name="key">The root note of the scale (e.g., C, F#, Bb)</param>
/// <param name="scaleName">The name of the scale (e.g., Major, Minor, Dorian)</param>
/// <param name="apiService">The API service for external data retrieval</param>
/// <returns>JSON string containing musical insights about the scale</returns>
/// <response code="200">Returns the musical insights</response>
/// <response code="400">If there's an error in retrieving the insights</response>
app.MapGet("/api/scale/insights", async ([FromQuery] string key, [FromQuery] string scaleName, IApiService apiService) =>
{
    var perplexityApiKey = app.Configuration["PERPLEXITY_API_KEY"] ??
    Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY");

    if (string.IsNullOrEmpty(perplexityApiKey))
    {
        return Results.BadRequest("Perplexity API key not found in configuration.");
    }

    if (string.IsNullOrEmpty(perplexityApiKey))
    {
        return Results.BadRequest("Perplexity API key not found in configuration.");
    }

    var promptLines = app.Configuration.GetSection("ScaleInsightsPrompt").Get<string[]>();
    if (promptLines == null || promptLines.Length == 0)
    {
        return Results.BadRequest("Scale insights prompt template not found in configuration.");
    }

    var prompt = string.Join("\n", promptLines);
    prompt = string.Format(prompt, key, scaleName);

    var apiConfig = new PerplexityApiConfiguration
    {
        Key = perplexityApiKey,
        BaseAddress = "https://api.perplexity.ai/chat/completions",
        Method = "POST",
        Model = "llama-3.1-sonar-large-128k-online",
        ParamSet = prompt
    };

    try
    {
        var response = await apiService.CallApiAsync(apiConfig);
        var responseData = JsonSerializer.Deserialize<JsonElement>(response);
        var insightsJson = responseData.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

        var jsonStart = insightsJson.IndexOf('{');
        var jsonEnd = insightsJson.LastIndexOf('}');
        var jsonString = insightsJson.Substring(jsonStart, jsonEnd - jsonStart + 1);

        return Results.Ok(jsonString);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Error: {ex.Message}");
    }
})
.WithName("GetScaleInsights")
.Produces<string>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

app.Run();