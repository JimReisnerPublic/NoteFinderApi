using Microsoft.AspNetCore.Mvc;
using NoteFinder.ExternalInfo.Service.Data;
using NoteFinder.ExternalInfo.Service;
using NoteFinder.Helpers;
using NoteFinder.Interfaces;
using NoteFinder.Service;
using System.Text.Json;

namespace NoteFinderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScaleController : ControllerBase
    {
        private readonly IApiService _apiService;
        private readonly IConfiguration _configuration;

        public ScaleController(IApiService apiService, IConfiguration configuration)
        {
            _apiService = apiService;
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieves the notes of a given scale in a specific key.
        /// </summary>
        /// <param name="key">The root note of the scale (e.g., C, F#, Bb)</param>
        /// <param name="scaleName">The name of the scale (e.g., Major, Minor, Dorian)</param>
        /// <returns>A collection of notes in the specified scale</returns>
        /// <response code="200">Returns the collection of notes in the scale</response>
        /// <response code="400">If the key or scale name is invalid</response>
        [HttpGet("notes")]
        [ProducesResponseType(typeof(NoteCollection), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetScaleNotes([FromQuery] string key, [FromQuery] string scaleName)
        {
            try
            {
                IInterval[] scaleIntervals = DefinitionsHelper.GetScaleIntervals(scaleName);
                NoteCollection noteCollection = new NoteCollection(key.ToUpper(), scaleIntervals);
                return Ok(noteCollection);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves musical insights about a specific scale in a given key.
        /// </summary>
        /// <param name="key">The root note of the scale (e.g., C, F#, Bb)</param>
        /// <param name="scaleName">The name of the scale (e.g., Major, Minor, Dorian)</param>
        /// <returns>JSON string containing musical insights about the scale</returns>
        /// <response code="200">Returns the musical insights</response>
        /// <response code="400">If there's an error in retrieving the insights</response>
        [HttpGet("insights")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScaleNotes([FromQuery] string key, [FromQuery] string scaleName)
        {
            var perplexityApiKey = _configuration["PERPLEXITY_API_KEY"] ??
                                    Environment.GetEnvironmentVariable("PERPLEXITY_API_KEY");

            if (string.IsNullOrEmpty(perplexityApiKey))
            {
                return BadRequest("Perplexity API key not found in configuration.");
            }

            var promptLines = _configuration.GetSection("ScaleInsightsPrompt").Get<string[]>();
            if (promptLines == null || promptLines.Length == 0)
            {
                return BadRequest("Scale insights prompt template not found in configuration.");
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
                var response = await _apiService.CallApiAsync(apiConfig);
                var responseData = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(response);
                var insightsJson = responseData.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

                var jsonStart = insightsJson.IndexOf('{');
                var jsonEnd = insightsJson.LastIndexOf('}');
                var jsonString = insightsJson.Substring(jsonStart, jsonEnd - jsonStart + 1);

                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
