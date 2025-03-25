NoteFinder API
A comprehensive .NET Core Web API for music theory operations including scales, chords, and musical insights.

Overview
NoteFinder API provides endpoints for musicians, developers, and music theory enthusiasts to:

Get notes for any scale in any key

Get chord information based on scale degrees

Get chord notes for specific chord types

Retrieve detailed musical insights about scales using AI

Getting Started
Prerequisites
.NET 8.0 SDK or later

An IDE (Visual Studio, VS Code, etc.)

A Perplexity API key (for the scale insights feature)

Installation
Clone the repository:

text
git clone https://github.com/yourusername/NoteFinder.git
Navigate to the project directory:

text
cd NoteFinder
Create your appsettings.json file from the template:

text
cp appsettings.template.json appsettings.json
Add your Perplexity API key to the appsettings.json file or set it as an environment variable:

json
"PERPLEXITY_API_KEY": "your-api-key-here"
Build the project:

text
dotnet build
Run the project:

text
dotnet run
The API will be available at https://localhost:7095.

API Endpoints
Scale Endpoints
Get Scale Notes
text
GET /api/Scale/notes?key={key}&scaleName={scaleName}
Returns a collection of notes in the specified scale.

Parameters:

key (string): The root note of the scale (e.g., C, F#, Bb)

scaleName (string): The name of the scale (e.g., Major, Minor, Dorian)

Get Scale Insights
text
GET /api/Scale/insights?key={key}&scaleName={scaleName}
Returns detailed musical insights about the specified scale, including structure, characteristics, applications, example songs, and common chord progressions.

Parameters:

key (string): The root note of the scale (e.g., C, F#, Bb)

scaleName (string): The name of the scale (e.g., Major, Minor, Dorian)

Chord Endpoints
Get Chord of Scale Degree
text
GET /api/Chord/chord-of-scale-degree?key={key}&scaleName={scaleName}&degree={degree}
Returns information about the chord at a specific scale degree in a given key and scale.

Parameters:

key (string): The root note of the scale (e.g., C, F#, Bb)

scaleName (string): The name of the scale (e.g., Major, Minor, Dorian)

degree (int): The scale degree (1-7) to get the chord for

Get Chord Notes
text
GET /api/Chord/notes?key={key}&chordName={chordName}
Returns a collection of notes in the specified chord.

Parameters:

key (string): The root note of the chord (e.g., C, F#, Bb)

chordName (string): The name of the chord (e.g., Major, Minor, Dominant7)

Configuration
The application uses appsettings.json for configuration. Sensitive information like API keys should be stored using .NET's User Secrets or environment variables:

bash
dotnet user-secrets set "PERPLEXITY_API_KEY" "your-api-key-here"
Project Structure
NoteFinder.Interfaces: Core interfaces for the application

NoteFinder.Service: Implementation of music theory services

NoteFinder.ExternalInfo.Service: Services for external API integration

NoteFinder.Helpers: Helper classes and utilities

Technologies Used
.NET 8.0

ASP.NET Core Web API

Swagger/OpenAPI

Perplexity AI API

Acknowledgments
Special thanks to the music theory community

Perplexity AI for providing the insights API
