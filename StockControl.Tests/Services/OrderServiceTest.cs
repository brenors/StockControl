using FluentAssertions;
using Moq;
using StockControl.Application.DTOs.Orders;
using StockControl.Application.Interfaces;
using StockControl.Application.Services;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Repositories;
using StockControl.Tests.Mocks;

namespace StockControl.Tests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task Should_Create_Order_When_Stock_Is_Available()
        {
            var orderRepo = new Mock<IOrderRepository>();
            var productRepo = new Mock<IProductRepository>();
            var stockRepo = new Mock<IStockRepository>();

            productRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Product { Price = 100 });

            stockRepo.Setup(x => x.GetAvailableQuantity(It.IsAny<Guid>()))
                .ReturnsAsync(10);

            var mapper = MapperMock.Create<Order, OrderResponse>(new OrderResponse());

            var service = new OrderService(orderRepo.Object, productRepo.Object, stockRepo.Object, mapper);

            var request = new CreateOrderRequest
            {
                CustomerDocument = "123",
                SellerName = "Seller",
                Items = new List<CreateOrderItemRequest>
                {
                    new() { ProductId = Guid.NewGuid(), Quantity = 2 }
                }
            };

            var result = await service.Create(request);

            result.Should().NotBeNull();
            orderRepo.Verify(x => x.AddOrderWithItemsAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async Task Should_Fail_When_Stock_Is_Insufficient()
        {
            var orderRepo = new Mock<IOrderRepository>();
            var productRepo = new Mock<IProductRepository>();
            var stockRepo = new Mock<IStockRepository>();

            productRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Product { Price = 100 });

            stockRepo.Setup(x => x.GetAvailableQuantity(It.IsAny<Guid>()))
                .ReturnsAsync(0);

            var mapper = MapperMock.Create<Order, OrderResponse>(new OrderResponse());

            var service = new OrderService(orderRepo.Object, productRepo.Object, stockRepo.Object, mapper);

            var request = new CreateOrderRequest
            {
                CustomerDocument = "123",
                SellerName = "Seller",
                Items = new List<CreateOrderItemRequest>
                {
                    new() { ProductId = Guid.NewGuid(), Quantity = 2 }
                }
            };

            Func<Task> act = async () => await service.Create(request);

            await act.Should().ThrowAsync<Exception>();
        }
    }
}
