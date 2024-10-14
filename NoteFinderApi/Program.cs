//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using NoteFinder.Interfaces;
using NoteFinder.Service;
using NoteFinder.Service.Definitions;
using NoteFinder.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

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
        noteCollection.SetProperlyNamedNotes();

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
        noteCollection.SetProperlyNamedNotes();

        return Results.Ok(noteCollection);
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Error: {ex.Message}");
    }
})
.WithName("GetChordNotes"); 

app.Run();