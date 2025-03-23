using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace NoteFinderTests
{
    public class IntegrationTestBase : WebApplicationFactory<Program>
    {
        protected readonly HttpClient _client;
        protected IConfiguration Configuration { get; }

        public IntegrationTestBase()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = configBuilder.Build();

            _client = CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri(Configuration["ApiSettings:BaseUrl"] ?? "http://localhost:7095")
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                config.AddConfiguration(Configuration);
            });

            return base.CreateHost(builder);
        }
    }


    public class NoteFinderControllerTests
    {
        private readonly HttpClient _client;

        public NoteFinderControllerTests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:7095")
            };
        }
        [Fact]
        public async Task GetScaleNotes_ReturnsCorrectNotes()
        {
            // Arrange
            var request = "/api/scale/notes?key=C&scaleName=ionian";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var noteCollection = await response.Content.ReadFromJsonAsync<NoteFinder.Service.NoteCollection>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(noteCollection);
            Assert.Equal(new List<string> { "C", "D", "E", "F", "G", "A", "B" }, noteCollection.GetProperlyNamedNotes());
        }

        [Fact]
        public async Task GetChordOfScaleDegree_ReturnsCorrectChord()
        {
            // Arrange
            //var request = "/api/chord-of-scale-degree?key=C&scaleName=ionian&degree=1";
            var request = "/api/chord/chord-of-scale-degree?key=C&scaleName=ionian&degree=1";


            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);
        }

        [Fact]
        public async Task GetChordNotes_ReturnsCorrectNotes()
        {
            // Arrange
            var request = "/api/chord/notes?key=C&chordName=major";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            var noteCollection = await response.Content.ReadFromJsonAsync<NoteFinder.Service.NoteCollection>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(noteCollection);
            Assert.Equal(new List<string> { "C", "E", "G" }, noteCollection.GetProperlyNamedNotes());
        }

        [Fact]
        public async Task GetScaleInsights_ReturnsOkResult()
        {
            // Arrange
            var request = "/api/scale/insights?key=C&scaleName=ionian";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetScaleNotes_InvalidKey_ReturnsBadRequest()
        {
            // Arrange
            var request = "/api/scale/notes?key=H&scaleName=ionian";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetChordNotes_InvalidChordName_ReturnsBadRequest()
        {
            // Arrange
            var request = "/api/chord/notes?key=C&chordName=invalidchord";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetScaleInsights_NoApiKey_ReturnsBadRequest()
        {
            // Arrange
            var request = "/api/scale/insights?key=C&scaleName=ionian";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //[Fact]
        //public async Task GetScaleInsights_NoPrompt_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var request = "/api/scale/insights?key=C&scaleName=ionian";

        //    // Act
        //    var response = await _client.GetAsync(request);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}
    }
}
