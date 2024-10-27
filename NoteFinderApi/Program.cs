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

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

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
.WithName("GetScaleNotes");

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
});

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
.WithName("GetChordNotes");

app.MapGet("/api/scale/insights", async ([FromQuery] string key, [FromQuery] string scaleName, IApiService apiService) =>
{
    var perplexityApiKey = app.Configuration["PERPLEXITY_API_KEY"];
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
.WithName("GetScaleInsights");

app.Run();