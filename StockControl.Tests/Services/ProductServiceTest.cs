using FluentAssertions;
using Moq;
using StockControl.Application.DTOs.Products;
using StockControl.Application.Interfaces;
using StockControl.Application.Services;
using StockControl.Domain.Entities;
using StockControl.Tests.Mocks;

namespace StockControl.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task Should_Create_Product()
        {
            var repo = new Mock<IProductRepository>();

            var mapper = MapperMock.Create<Product, ProductResponse>(new ProductResponse());

            var service = new ProductService(repo.Object, mapper);

            var request = new ProductRequest
            {
                Name = "Produto",
                Description = "Desc",
                Price = 100
            };

            var result = await service.Create(request);

            result.Should().NotBeNull();
            repo.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Should_Filter_Products()
        {
            var repo = new Mock<IProductRepository>();

            repo.Setup(x => x.FilterAsync(null, null, null))
                .ReturnsAsync(new List<Product> { new Product() });

            var mapper = MapperMock.Create<Product, ProductResponse>(new ProductResponse());

            var service = new ProductService(repo.Object, mapper);

            var result = await service.Filter(null, null, null);

            result.Should().NotBeEmpty();
        }
    }
}
