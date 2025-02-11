using CustomerService.DTOs;
using OrderService.Clients;
using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;
using System.Diagnostics.Eventing.Reader;

namespace OrderService.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _repository;
        private readonly ICustomerServiceClient _customerServiceClient;
        private readonly IProductServiceClient _productServiceClient;

        public OrdersService(IOrderRepository repository, ICustomerServiceClient customerServiceClient, IProductServiceClient productServiceClient)
        {
            _repository = repository;
            _customerServiceClient = customerServiceClient;
            _productServiceClient = productServiceClient;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() =>
            await _repository.GetAllAsync();

        public async Task<OrderDTO?> GetOrderByIdAsync(string id)
        {
            if (await _repository.GetByIdAsync(id) is Order order)
            { 
            var dto = new OrderDTO()
            {
                Id = order.Id,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate.ToString()
            };

            if (await _customerServiceClient.GetCustomerAsync(order.CustomerId) is CustomerDTO customer
                && await _productServiceClient.GetProductAsync(order.ProductId) is ProductDTO product)
            {
                dto.Customer = customer;
                dto.Product = product;
            }
            else
            {
                dto.Customer.Id = order.CustomerId;
                dto.Product.Id = order.ProductId;
            }
            return dto;
            }
            return default;
        }
            

        public async Task<OrderDTO?> AddOrderAsync(OrderPostDTO order)
        {
            var orderToAdd = new Order() {
                OrderDate = order.OrderDate != null ? DateTime.Parse(order.OrderDate) : default,
                CustomerId = order.CustomerId,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
            };
            if (await _customerServiceClient.GetCustomerAsync(order.CustomerId) is CustomerDTO customer
                && await _productServiceClient.GetProductAsync(order.ProductId) is ProductDTO product)
            { 
            await _repository.AddAsync(orderToAdd);

            return new OrderDTO() {
                Id = orderToAdd.Id,
                Quantity = orderToAdd.Quantity,
                OrderDate = orderToAdd.OrderDate.ToString(),
                Customer = customer,
                Product = product
            };
            }
            return default;
        }

        public async Task UpdateOrderAsync(Order order) =>
            await _repository.UpdateAsync(order);

        public async Task DeleteOrderAsync(string id) =>
            await _repository.DeleteAsync(id);
    }
}
