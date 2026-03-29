using AutoMapper;
using StockControl.Application.DTOs.Stocks;
using StockControl.Application.Interfaces;
using StockControl.Common.Validator;
using StockControl.Domain.Entities;

namespace StockControl.Application.Services
{
    public class StockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public StockService(
            IStockRepository stockRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<StockResponse> AddStock(StockEntryRequest request)
        {
            DomainValidator.Assert(request.Quantity != 0, "Quantity must not be zero");

            var product = await _productRepository.GetByIdAsync(request.ProductId);
            DomainValidator.Assert(product != null, "Product not found");

            if (request.Quantity > 0)
            {
                var availableQuantity = await _stockRepository.GetAvailableQuantity(request.ProductId);
                DomainValidator.Assert(request.InvoiceNumber.IsNotNullOrWhiteSpace(), "Invoice number is required for stock entry");
            }

            var stock = new Stock
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                InvoiceNumber = request.InvoiceNumber,
                CreatedAt = DateTime.UtcNow
            };

            await _stockRepository.AddAsync(stock);
            await _stockRepository.SaveChangesAsync();

            return _mapper.Map<StockResponse>(stock);
        }

        public async Task<int> GetAvailable(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            DomainValidator.Assert(product != null, "Product not found");
            return await _stockRepository.GetAvailableQuantity(productId);
        }
    }
}
