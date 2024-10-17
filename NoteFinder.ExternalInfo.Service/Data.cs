using System.Text.Json;
using System.Text;

namespace NoteFinder.ExternalInfo.Service.Data
{
    public interface IApiConfiguration
    {
        HttpRequestMessage PrepareRequest();
        string ApiId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string BaseAddress { get; set; }
        string Key { get; set; }
        string Method { get; set; }
        string ParamSet { get; set; }
    }

    public class PerplexityApiConfiguration : IApiConfiguration
    {
        public string ApiId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BaseAddress { get; set; }
        public string Key { get; set; }
        public string Model { get; set; }
        public string Method { get; set; }
        public string ParamSet { get; set; }

        public HttpRequestMessage PrepareRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BaseAddress);
            request.Headers.Add("Authorization", $"Bearer {Key}");

            var payload = new
            {
                //TODO: Make Configuraable
                model = "llama-3.1-sonar-large-128k-online",
                messages = new[]
                {
                new { role = "system", content = "You are a helpful assistant that provides news summaries." },
                new { role = "user", content = ParamSet }
            }
            };
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            return request;
        }
    }
}
