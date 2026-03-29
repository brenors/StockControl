using AutoMapper;
using StockControl.Application.DTOs.Products;
using StockControl.Application.Interfaces;
using StockControl.Common.Validator;
using StockControl.Domain.Entities;

namespace StockControl.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Create(ProductRequest request)
        {
            var validator = DomainValidator.Contract();

            validator
                .Assert(request.Name.IsNotNullOrWhiteSpace(), "Name is required")
                .Assert(request.Description.IsNotNullOrWhiteSpace(), "Description is required")
                .Assert(request.Price.IsPositive(), "Price must be greater than zero")
                .Validate();

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<IEnumerable<ProductResponse>> Filter(string? name, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _repository.FilterAsync(name, minPrice, maxPrice);
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse> GetById(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);

            DomainValidator.Assert(product != null, "Product not found");

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<IEnumerable<ProductResponse>> GetByName(string name)
        {
            var products = await _repository.GetByNameAsync(name);

            DomainValidator.Assert(products.Any(), "No products found");

            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task Update(Guid id, ProductRequest request)
        {
            var product = await _repository.GetByIdAsync(id);

            DomainValidator.Assert(product != null, "Product not found");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.UpdatedAt = DateTime.UtcNow;

            _repository.Update(product);
            await _repository.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);

            DomainValidator.Assert(product != null, "Product not found");

            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;

            _repository.Update(product);
            await _repository.SaveChangesAsync();
        }
    }
}
