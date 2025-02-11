using CustomerService.DTOs;

namespace OrderService.DTOs
{
    public class OrderDTO
    {
        public string? Id { get; set; }
        public int Quantity { get; set; }
        public string OrderDate { get; set; }
        public ProductDTO Product { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
