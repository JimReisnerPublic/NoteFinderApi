using Microsoft.Extensions.Logging;
using NoteFinder.ExternalInfo.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteFinder.ExternalInfo.Service
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;

        public ApiService(HttpClient httpClient, ILogger<ApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> CallApiAsync(PerplexityApiConfiguration perplexityApiConfig)
        {

            using var request = perplexityApiConfig.PrepareRequest();
            try
            {
                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling API for Perplexity");
                throw;
            }
        }

    }
}
