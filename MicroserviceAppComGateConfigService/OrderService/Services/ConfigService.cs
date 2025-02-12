using OrderService.Configurations;
using System.Text.Json;

namespace OrderService.Services
{
    public class ConfigService
    {

        private readonly HttpClient _httpClient;

        public ConfigService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MongoDbSettings?> GetMongoSettingsAsync(string serviceName)
        {

            var response = await _httpClient.GetAsync($"{serviceName}/default");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            using (var document = JsonDocument.Parse(content))
            {
                var root = document.RootElement;

                if (root.TryGetProperty("propertySources", out JsonElement propertySources))
                {
                    var firstSource = propertySources[0].GetProperty("source");

                    if (firstSource.TryGetProperty("MongoDbSettings.ConnectionString", out JsonElement connectionString) && firstSource.TryGetProperty("MongoDbSettings.DatabaseName", out JsonElement databaseName))
                    {
                        return new MongoDbSettings()
                        {
                            ConnectionString = connectionString.GetString(),
                            DatabaseName = databaseName.GetString()
                        };
                    }
                }
            }

            return default;


        }


    }
}
