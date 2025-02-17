using CSharp.Advanced.Interfaces;
using CSharp.Advanced.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSharp.Advanced.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _ordersService;

        public OrdersController(IOrderService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpPost]
        public IActionResult Post(Order order)
        {
            var placeOrderResult = _ordersService.PlaceOrder(order);

            return placeOrderResult.Match<IActionResult>(
                receipt => Ok(receipt),
                error => StatusCode(500, new { error = error.ToString() }));
        }
    }
}
