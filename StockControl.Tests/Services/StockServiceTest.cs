using FluentAssertions;
using Moq;
using StockControl.Application.DTOs.Stocks;
using StockControl.Application.Interfaces;
using StockControl.Application.Services;
using StockControl.Domain.Entities;
using StockControl.Tests.Mocks;

namespace StockControl.Tests.Services
{
    public class StockServiceTests
    {
        [Fact]
        public async Task Should_Add_Stock()
        {
            var stockRepo = new Mock<IStockRepository>();
            var productRepo = new Mock<IProductRepository>();

            productRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Product());

            var mapper = MapperMock.Create<Stock, StockResponse>(new StockResponse());

            var service = new StockService(stockRepo.Object, productRepo.Object, mapper);

            var request = new StockEntryRequest
            {
                ProductId = Guid.NewGuid(),
                Quantity = 10,
                InvoiceNumber = "123"
            };

            var result = await service.AddStock(request);

            result.Should().NotBeNull();
            stockRepo.Verify(x => x.AddAsync(It.IsAny<Stock>()), Times.Once);
        }

        [Fact]
        public async Task Should_Return_Available_Stock()
        {
            var stockRepo = new Mock<IStockRepository>();
            var productRepo = new Mock<IProductRepository>();

            productRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Product());

            stockRepo.Setup(x => x.GetAvailableQuantity(It.IsAny<Guid>()))
                .ReturnsAsync(10);

            var service = new StockService(stockRepo.Object, productRepo.Object, null);

            var result = await service.GetAvailable(Guid.NewGuid());

            result.Should().Be(10);
        }
    }
}
