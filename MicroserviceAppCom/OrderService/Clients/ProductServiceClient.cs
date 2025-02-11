
using OrderService.DTOs;

namespace OrderService.Clients
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:7000");

        }

        public async Task<ProductDTO?> GetProductAsync(int productId)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductDTO>($"/api/products/{productId}");
            return response;

        }
    }
}
