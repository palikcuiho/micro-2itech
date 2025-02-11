
using CustomerService.DTOs;

namespace OrderService.Clients
{
    public class CustomerServiceClient : ICustomerServiceClient
    {

        private readonly HttpClient _httpClient;

        public CustomerServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000");

        }

        public async Task<CustomerDTO?> GetCustomerAsync(int customerId)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomerDTO>($"/api/customer/{customerId}");
            return response;
           
        }
    }
}
