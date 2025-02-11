using OrderService.DTOs;

namespace OrderService.Clients
{
    public interface IProductServiceClient
    {
        Task<ProductDTO?> GetProductAsync(int productId);


    }
}
