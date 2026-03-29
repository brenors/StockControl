using AutoMapper;
using StockControl.Application.DTOs.Orders;
using StockControl.Application.Interfaces;
using StockControl.Common.Validator;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Repositories;

namespace StockControl.Application.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IStockRepository stockRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Create(CreateOrderRequest request)
        {
            DomainValidator.Assert(request.Items.Any(), "Order must have items");

            decimal totalAmount = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                DomainValidator.Assert(product != null, $"Product ID {item.ProductId} not found");

                var available = await _stockRepository.GetAvailableQuantity(item.ProductId);
                DomainValidator.Assert(
                    available >= item.Quantity,
                    $"Insufficient stock for product {product.Name}"
                );

                var itemTotal = item.Quantity * product.Price;
                totalAmount += itemTotal;

                orderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerDocument = request.CustomerDocument,
                SellerName = request.SellerName,
                TotalAmount = totalAmount,
                CreatedAt = DateTime.UtcNow,
                Items = orderItems
            };

            foreach (var item in request.Items)
            {
                await _stockRepository.DecreaseStock(item.ProductId, item.Quantity);
            }

            await _orderRepository.AddOrderWithItemsAsync(order);

            return _mapper.Map<OrderResponse>(order);
        }
    }
}
