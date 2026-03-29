using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockControl.Application.DTOs.Stocks;
using StockControl.Application.Services;

namespace StockControl.API.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly StockService _service;

        public StockController(StockService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddStock([FromBody] StockEntryRequest request)
        {
            var result = await _service.AddStock(request);
            return Ok(result);
        }

        [HttpGet("{productId:guid}/available")]
        public async Task<IActionResult> GetAvailable(Guid productId)
        {
            var quantity = await _service.GetAvailable(productId);
            return Ok(quantity);
        }
    }
}
