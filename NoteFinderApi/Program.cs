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

builder.Services.AddControllers(); // Add this line
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

app.MapControllers(); // Add this line to map the controllers


app.Run();

