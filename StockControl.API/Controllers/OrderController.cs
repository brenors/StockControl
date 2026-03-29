using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockControl.Application.DTOs.Orders;
using StockControl.Application.Services;

namespace StockControl.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize(Roles = "Seller")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;

        public OrderController(OrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var result = await _service.Create(request);
            return Ok(result);
        }
    }
}
