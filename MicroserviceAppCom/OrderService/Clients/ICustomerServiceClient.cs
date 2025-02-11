using CustomerService.DTOs;

namespace OrderService.Clients
{
    public interface ICustomerServiceClient
    {

        Task<CustomerDTO?> GetCustomerAsync(int customerId);

    }
}
