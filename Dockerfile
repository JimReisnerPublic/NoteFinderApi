# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy only the main project file (which includes references to other projects)
COPY ./NoteFinderApi/*.csproj ./NoteFinderApi/

# Restore dependencies for NoteFinderApi (this will also restore referenced projects)
RUN dotnet restore ./NoteFinderApi/NoteFinderApi.csproj

# Copy all source code (including referenced projects)
COPY . .

# Build and publish the application
RUN dotnet publish ./NoteFinderApi/NoteFinderApi.csproj -c Release -o /app/publish

# Use a lightweight runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory in the container
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose both HTTP (80) and HTTPS (443) ports
EXPOSE 80
EXPOSE 443

# Set environment variable to expose app on port 80 (HTTP) and 443 (HTTPS)
ENV ASPNETCORE_URLS="http://+:80;https://+:443"
ENV ASPNETCORE_ENVIRONMENT="Development"

ENTRYPOINT ["dotnet", "NoteFinderApi.dll"]