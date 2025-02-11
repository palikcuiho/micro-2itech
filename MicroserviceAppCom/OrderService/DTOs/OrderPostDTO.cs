namespace OrderService.DTOs
{
    public class OrderPostDTO
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public string OrderDate { get; set; } = DateTime.UtcNow.ToString();
    }
}
