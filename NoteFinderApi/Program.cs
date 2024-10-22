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

//app.MapGet("/api/scale/insights", async ([FromQuery] string key, [FromQuery] string scaleName) =>
//{
//    var perplexityApiKey = app.Configuration["PERPLEXITY_API_KEY"];

//    if (string.IsNullOrEmpty(perplexityApiKey))
//    {
//        return Results.BadRequest("Perplexity API key not found in configuration.");
//    }

//    Console.WriteLine($"API Key: {perplexityApiKey}"); // Remove in production

//    try
//    {
//        var client = new HttpClient();
//        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {perplexityApiKey}");
//       // client.Headers.Add("Authorization", $"Bearer {Key}");

//        var prompt = $"Provide information about the {key} {scaleName} scale, including:\n" +
//                     "1. Structure and Characteristics\n" +
//                     "2. Musical Applications\n" +
//                     "3. Example Songs\n" +
//                     "4. Common Chord Progressions\n" +
//                     "Please format the response as a JSON object with keys: structure, characteristics, applications, exampleSongs, chordProgressions.";

//        var requestContent = JsonSerializer.Serialize(new
//        {
//            model = "llama-3.1-sonar-large-128k-online",
//            messages = new[]
//            {
//                new { role = "user", content = prompt }
//            }
//        });
//        Console.WriteLine($"Request Content: {requestContent}"); // Remove in production

//        var content = new StringContent(requestContent, System.Text.Encoding.UTF8, "application/json");

//        var response = await client.PostAsync("https://api.perplexity.ai/chat/completions", content);
//        var responseString = await response.Content.ReadAsStringAsync();

//        if (!response.IsSuccessStatusCode)
//        {
//            Console.WriteLine($"Response Status Code: {response.StatusCode}");
//            Console.WriteLine($"Response Headers: {response.Headers}");
//            Console.WriteLine($"Response Content: {responseString}");
//            return Results.BadRequest($"Error from Perplexity API: {responseString}");
//        }

//        var responseData = JsonSerializer.Deserialize<JsonElement>(responseString);
//        var insightsJson = responseData.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

//        var jsonStart = insightsJson.IndexOf('{');
//        var jsonEnd = insightsJson.LastIndexOf('}');
//        var jsonString = insightsJson.Substring(jsonStart, jsonEnd - jsonStart + 1);

//        return Results.Ok(jsonString);
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Exception: {ex}");
//        return Results.BadRequest($"Error: {ex.Message}");
//    }
//})
//.WithName("GetScaleInsights");

app.Run();